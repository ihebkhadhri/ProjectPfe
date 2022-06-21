using ConnexionMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Text;

namespace ProjectPfe.Services.libs

{
    public class GridFsStockTemplate
    {
        private static GridFSBucket bucket;
        private  MongoGridFS _gridFs;
        public GridFsStockTemplate(
            IOptions<DbPfeDatabaseSettings> DatabaseSettings)
        {
            var mongoClient = new MongoClient(
                DatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DatabaseSettings.Value.DatabaseName);


            var client = new MongoServer(new MongoServerSettings());
           var mg=  client.GetDatabase(DatabaseSettings.Value.DatabaseName);


            _gridFs = mg.GridFS;

            bucket = new GridFSBucket(mongoDatabase);

        }

        public  ObjectId UploadFile(IFormFile file)
        {

            /*

            var ms = new MemoryStream();
                    
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string s = Convert.ToBase64String(fileBytes);


                
                    return
                    bucket.UploadFromStream(file.FileName, ms);
               */


            using (var stream = file.OpenReadStream())
            {
                return MongoUpload("Template_" + file.FileName, stream);
                
            }

        }


        public ObjectId UploadFileXml(IFormFile file)
        {

            using (var stream = file.OpenReadStream())
            {
                return MongoUploadXmlFile("Input_" + file.FileName, stream);

            }

        }

        private  ObjectId MongoUpload(string filename, Stream stream)
        {

            var options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument
                {
                    {"ContentType","pdf"}
                }
            };
            var id =  bucket.UploadFromStream(filename, stream, options);
            return id;
            //if (id == null)
            //    return false;
            //else
            //    return true;
        }
        private ObjectId MongoUploadXmlFile(string filename, Stream stream)
        {

            var options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument
                {
                    {"ContentType","text/xml"}
                }
            };
            var id = bucket.UploadFromStream(filename, stream, options);
            return id;
            //if (id == null)
            //    return false;
            //else
            //    return true;
        }

        public  string DownloadFile( String id)
        {
          

            var x = bucket.DownloadAsBytes(new ObjectId(id));

            string s = Convert.ToBase64String(x);

            return s;
           
        }

        public string DownloadFileByName(String name)
        {


            var x = bucket.DownloadAsBytesByName(name+".pdf");

            string s = Convert.ToBase64String(x);

            return s;

        }

        public  byte[] GetFileBytes(String id)
        {
            

            var x = bucket.DownloadAsBytes(new ObjectId(id));
           
            return x;
            
        }

        public ObjectId CreateFileByBytes(String filename,byte[] bytes)
        {


            var x = bucket.UploadFromBytes(filename,bytes);

            return x;

        }

        public ObjectId createRapport( String idOriginalRtf,Integration integration)
        {

           var file= bucket.OpenDownloadStream(new ObjectId(idOriginalRtf));
          

            StreamReader sr = new StreamReader(file, Encoding.Default);

            String lines = sr.ReadToEnd();
            byte[] bytes = Encoding.Default.GetBytes(lines);
            string TextOriginal = Encoding.UTF8.GetString(bytes);

            string TextNewRapport = TextOriginal.Replace("##Nom##", integration.Nom).Replace("##Nationalite##",integration.Nationalite)
           .Replace("##prenom##", integration.Prenom).Replace("##Age##", integration.Age.ToString()).Replace("##DateNaissance##", integration.DateNaissance)
           .Replace("##sex##", integration.Sex)
           .Replace("##prixunitaire##", integration.PrixUnitaire)
           .Replace("##adresse##", integration.Adresse);

            foreach(var element in integration.Titres)
            {
                String titre = "##titre" + element.order + "##";
                 TextNewRapport = TextNewRapport.Replace(titre, element.libelle);
                foreach (var souselement in element.Sous_titres)
                {
                    String soustitre = "##soustitre" + element.order + "##";
                    TextNewRapport= TextNewRapport.Replace(soustitre, souselement.libelle);
                }
            }

            foreach (var element in integration.Paragraphes)
            {
                TextNewRapport = TextNewRapport.Replace("##paragraphe" + element.order + "##", element.libelle);
                foreach (var souselement in element.Sous_paragraphe)
                {
                    TextNewRapport = TextNewRapport.Replace("##sousparagraphe" + souselement.order + "##", souselement.libelle);
                }
            }

            byte[] bytesNewRapport = Encoding.Default.GetBytes(TextNewRapport);
          var objidRtfUser=  bucket.UploadFromBytes(integration.Id + ".rtf", bytesNewRapport);

            return objidRtfUser;



            sr.Close();
           
        }

        public  Stream GetFile(ObjectId id)
        {
            var file = _gridFs.FindOneById(id);
            return file.OpenRead();
        }

        public void removeFile(string id)
        {
            bucket.Delete(new ObjectId( id));
        }

        public ObjectId AddFile(Stream fileStream, string fileName)
        {
            var fileInfo = _gridFs.Upload(fileStream, fileName);
            return (ObjectId)fileInfo.Id;
        }

        public GridFSDownloadStream openfile(ObjectId Id)
        {
            return bucket.OpenDownloadStream(Id);
        }

        public void removefile(ObjectId Id)
        {
             bucket.Delete(Id);
        }


        public string findarchive(string id)
        {
            var bytes = bucket.DownloadAsBytes(new ObjectId(id));
            return Convert.ToBase64String(bytes);
        }

        public string findainput(string id)
        {
            var bytes = bucket.DownloadAsBytes(new ObjectId(id));
            return Convert.ToBase64String(bytes);
        }


    }



    }

