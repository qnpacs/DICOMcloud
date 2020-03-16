using Amazon.S3.Model;
using DICOMcloud.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DICOMcloud.AWS.Storage
{
    public class AWSContainer : IStorageContainer
    {
        //private S3Bucket _s3Bucket;
        string _folderPath;
        private StorageSetting _storageSetting;
        public AWSContainer(string folderPath)
        {
            _folderPath = folderPath;
            _storageSetting = new StorageSetting();
        }
        public string Connection
        {
            get
            {
                return _folderPath;
            }
        }

        public void Delete()
        {

            throw new NotImplementedException();
        }

        public IStorageLocation GetLocation(string name = null, IMediaId id = null)
        {
            return new AWSLocation(name, id);
        }

        public IEnumerable<IStorageLocation> GetLocations(string key)
        {
            var obj = _storageSetting.AmazonS3Client.ListObjects(_storageSetting.BucketName, key);
            foreach (var item in obj.S3Objects)
            {
                if (!item.Key.EndsWith("/"))
                {
                    yield return GetLocation(item.Key);
                }
            }
            //throw new NotImplementedException();
        }

        public bool LocationExists(string key)
        {
            return _storageSetting.AmazonS3Client.ListObjects(_storageSetting.BucketName, key).S3Objects.Count > 0;
        }
    }
}
