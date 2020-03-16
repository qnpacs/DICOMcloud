using Amazon.S3;
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
    public class AWSStorageService : MediaStorageService
    {
        private AmazonS3Client _amazonS3Client;
        private IKeyProvider _keyProvider;
        StorageSetting storageSetting;


        public AWSStorageService()
        {
            storageSetting = new StorageSetting();

            _amazonS3Client = storageSetting.AmazonS3Client;
            _keyProvider = new AWSKeyProvider();
        }

        protected override bool ContainerExists(string containerName)
        {
            containerName = containerName + "/";
            return _amazonS3Client.ListObjects(storageSetting.BucketName, containerName)
               .S3Objects.Count() > 0;
        }

        protected override IStorageContainer GetContainer(string containerKey)
        {
            //  containerKey = containerKey.Trim('/') + "/";
            var folders = _amazonS3Client.ListObjects(storageSetting.BucketName, containerKey);
            if (folders.S3Objects.Where(c => c.Key == containerKey).Count() == 0)
            {
                PutObjectRequest putObjectRequest = new PutObjectRequest();
                putObjectRequest.BucketName = storageSetting.BucketName;
                putObjectRequest.Key = containerKey + "/";
                var res = _amazonS3Client.PutObject(putObjectRequest);
            }
            return new AWSContainer(GetStoragePath(containerKey));
        }

        protected override IEnumerable<IStorageContainer> GetContainers(string containerKey)
        {
            var folders = _amazonS3Client.ListObjects(storageSetting.BucketName, containerKey);
            foreach (var item in folders.S3Objects)
            {
                if (item.Key.EndsWith("/"))
                {
                    yield return GetContainer(item.Key);
                }
            }

            //    throw new NotImplementedException();
        }

        protected override IKeyProvider GetKeyProvider()
        {
            return _keyProvider;

        }

        protected virtual string GetStoragePath(string folderName)
        {
            return folderName;
            // return Path.Combine(bucketName, folderName);
        }
    }
}
