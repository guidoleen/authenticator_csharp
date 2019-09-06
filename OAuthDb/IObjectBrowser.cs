using System;
using System.Collections.Generic;

public interface IObjectBrowser
{
    Dictionary<String, Object> GetObjectList();
    String GetObjectId();
    String GetObjectIdName();
}
