﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nucelotidz.storage.queue.Serializers
{
    public interface ISerializer
    {
        string Serialize<T>(T dataObject);
        T Deserialize<T>(string data);
    }
}
