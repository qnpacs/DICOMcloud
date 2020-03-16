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
    public class AWSLocation : ObservableStorageLocation, ISelfSignedUrlStorageLocation
    {
        private IMediaId _mediaId;
        private AmazonS3Client _amazonS3Client;
        private StorageSetting _storageSetting;
        string _fileName;
        public AWSLocation(string fileName, IMediaId id = null)
        {
            _mediaId = id;
            _fileName = fileName.Replace("\\", @"/");
            _storageSetting = new StorageSetting();

            _amazonS3Client = _storageSetting.AmazonS3Client;
            GetObjectResponse = DoGetObjectResponse();
        }

        private GetObjectResponse GetObjectResponse { get; set; }

        public override string ContentType => GetObjectResponse.Headers.ContentType;

        public override string ID => GetObjectResponse.Key;

        public override IMediaId MediaId => _mediaId;

        public override string Metadata { get; set; }

        public override string Name => GetObjectResponse.Key;

        public override bool Exists()
        {
            throw new NotImplementedException();
        }

        public Uri GetReadUrl(DateTimeOffset? startTime, DateTimeOffset? expiryTime)
        {
            throw new NotImplementedException();
        }

        public override long GetSize()
        {
            return GetObjectResponse.ContentLength;
        }

        public Uri GetWriteUrl(DateTimeOffset? startTime, DateTimeOffset? expiryTime)
        {
            throw new NotImplementedException();
        }

        protected override void DoDelete()
        {
            throw new NotImplementedException();
        }

        protected override Stream DoDownload()
        {
            MemoryStream memoryStream = new MemoryStream();

            using (Stream responseStream = GetObjectResponse.ResponseStream)
            {
                responseStream.CopyTo(memoryStream);
            }

            return memoryStream;
        }

        protected override void DoDownload(Stream stream)
        {
            using (Stream responseStream = GetObjectResponse.ResponseStream)
            {
                responseStream.CopyTo(stream);
            }
        }

        protected override Stream DoGetReadStream()
        {
            MemoryStream memoryStream = new MemoryStream();

            using (Stream responseStream = GetObjectResponse.ResponseStream)
            {
                responseStream.CopyTo(memoryStream);
            }

            return memoryStream;

        }

        protected override void DoUpload(string fileName, string contentType)
        {

        }

        protected override void DoUpload(byte[] buffer, string contentType)
        {
            var stream = new MemoryStream(buffer);
            DoUpload(stream, contentType); ;
            //   throw new NotImplementedException();
        }

        protected override void DoUpload(Stream stream, string contentType)
        {
            PutObjectRequest putObjectRequest = new PutObjectRequest();
            putObjectRequest.BucketName = _storageSetting.BucketName;
            putObjectRequest.Key = _fileName;//System.IO.Path.GetDirectoryName(_fileName).Replace("\\", @"/") + @"/";
            putObjectRequest.InputStream = stream;
            putObjectRequest.ContentType = contentType;
            putObjectRequest.AutoCloseStream = false;
            var response = _amazonS3Client.PutObject(putObjectRequest);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                GetObjectResponse = DoGetObjectResponse();
            }
        }

        private GetObjectResponse DoGetObjectResponse()
        {
            GetObjectRequest getObjectRequest = new GetObjectRequest
            {
                BucketName = _storageSetting.BucketName,
                Key = _fileName
            };
            return _amazonS3Client.GetObject(getObjectRequest);
        }
    }
}
