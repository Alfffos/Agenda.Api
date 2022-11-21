using Agenda_api.Entities;

namespace Agenda_api.Repository
{
    public class ContactRepository
    {
        public static List<Contact> FakeContacts = new List<Contact>()      //Aca estoy creando 2 Fake Contacts para poder usarlo de prueba.
        {
            new Contact()
            {
                Name = "Pablo",
                CelularNumber = 12345,
                Id= 1,
            },                                  // Esto lo tengo que cambiar para que el repository use la base de datos en vez de estos 2 FakeContacts.
            new Contact()
            {
                Name = "Maria",
                CelularNumber = 123456,
                Id=2,
            }
        };
        public List<Contact> GetAll()                 //En esta linea creo una lista GetAll() que me de vuelve en lista los Fake contacts de arriba.
        {
            return FakeContacts;
        }
    }
}
