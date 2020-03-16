using DICOMcloud.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DICOMcloud.AWS.Storage
{
    public class AWSKeyProvider : IKeyProvider
    {
        public virtual string GetContainerName(string key)
        {
            key = key.TrimStart(GetLogicalSeparator().ToCharArray());

            int index = key.LastIndexOf(GetLogicalSeparator());
            index = (index > -1 ? index : key.Length);

            return key.Substring(0, index);
        }

        public virtual string GetLocationName(string key)
        {
            return key;
            //key = key.TrimStart(GetLogicalSeparator().ToCharArray());

            //int index = key.IndexOf(GetLogicalSeparator());

            //index = (index > -1 ? index : 0);

            //key = key.Substring(index, key.Length - index);

            //return key + "/";// key.TrimStart(GetLogicalSeparator().ToCharArray());
        }
        //public virtual string GetContainerName(string key)
        //{
        //    int index = key.LastIndexOf(GetLogicalSeparator());

        //    if (index == -1)
        //    {
        //        return key;
        //    }
        //    else
        //    {
        //        return key.Substring(0, index);
        //    }
        //}

        //public virtual string GetLocationName(string key)
        //{
        //    int index = key.LastIndexOf(GetLogicalSeparator());


        //    if (index == -1)
        //    {
        //        return string.Empty;
        //    }
        //    else
        //    {
        //        //return key;
        //        return key.Substring(key.LastIndexOf(GetLogicalSeparator()) + 1);
        //    }
        //}

        public virtual string GetLogicalSeparator()
        {
            return "/";
        }

        public virtual string GetStorageKey(IMediaId id)
        {
            return string.Join(GetLogicalSeparator(), id.GetIdParts());
            //return Path.Combine(id.GetIdParts());
        }
        //public virtual string GetContainerName(string key)
        //{
        //    return key;

        //    //key = key.TrimStart(GetLogicalSeparator().ToCharArray());

        //    //int index = key.IndexOf(GetLogicalSeparator());
        //    //index = (index > -1 ? index : key.Length);

        //    //return key.Substring(0, index);
        //}

        //public virtual string GetLocationName(string key)
        //{
        //    key = key.TrimStart(GetLogicalSeparator().ToCharArray());

        //    int index = key.IndexOf(GetLogicalSeparator());

        //    index = (index > -1 ? index : 0);

        //    key = key.Substring(index, key.Length - index);

        //    return key.TrimStart(GetLogicalSeparator().ToCharArray());
        //}

        //public string GetLogicalSeparator()
        //{
        //    return "/";
        //}

        //public string GetStorageKey(IMediaId key)
        //{
        //    return string.Join(GetLogicalSeparator(), key.GetIdParts());
        //}
    }
}
