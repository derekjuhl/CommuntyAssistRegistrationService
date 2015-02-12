using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDonorRegisterService" in both code and config file together.
[ServiceContract]
public interface IDonorRegisterService
{
    [OperationContract]
    void Register(Person p, PersonAddress pa);
    [OperationContract]
    int Login(string username, string password);
    [OperationContract]
    void Donate(Donation d);
}
