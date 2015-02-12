using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DonorRegisterService" in code, svc and config file together.
public class DonorRegisterService : IDonorRegisterService
{
    CommunityAssistEntities communityAssistDb 
        = new CommunityAssistEntities();

    public void Register(Person p, PersonAddress pa)
    {
        PasswordHash phash = new PasswordHash();
        KeyCode keycode = new KeyCode();
        int key = keycode.GetKeyCode();
        byte[] hash = phash.HashIt(p.PersonPlainPassword, key.ToString());

        Person person = new Person();
        person.PersonFirstName = p.PersonFirstName;
        person.PersonLastName = p.PersonLastName;
        person.Personpasskey = key;
        person.PersonUsername = p.PersonUsername;
        person.PersonPlainPassword = p.PersonPlainPassword;
        person.PersonUserPassword = hash;
        person.PersonEntryDate = DateTime.Now;
        communityAssistDb.People.Add(person);

        PersonAddress pAddress = new PersonAddress();
        pAddress.Person = person;
        pAddress.Apartment = pa.Apartment;
        pAddress.Street = pa.Street;
        pAddress.City = pa.City;
        pAddress.State = pa.State;
        pAddress.Zip = pa.Zip;
        communityAssistDb.PersonAddresses.Add(pAddress);

        communityAssistDb.SaveChanges();

    }

    public int Login(string username, string password)
    {
        LoginClass login = new LoginClass(password, username);
        int key = login.ValidateLogin();
        return key;
        
    }

    public void Donate(Donation d)
    {
        int key = (int)d.PersonKey;
        Donation donate = new Donation();
        donate.DonationDate = DateTime.Now;
        donate.DonationAmount = d.DonationAmount;
        donate.PersonKey = key;
        communityAssistDb.Donations.Add(donate);
        communityAssistDb.SaveChanges();
    }
}
