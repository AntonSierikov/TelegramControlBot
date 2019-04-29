using System;
using ControlBot.Core.Abstract;

namespace ControlBot.Core.Entities
{
    public class ControlUser : DbObject<Int32>
    {

        //----------------------------------------------------------------//

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String UserName { get; set; }

        //----------------------------------------------------------------//

        public ControlUser() {}

        //----------------------------------------------------------------//
                    
        public ControlUser(Int32 userId, String firstName, String lastName, String userName)
        {
            Id = userId;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
        }

        //----------------------------------------------------------------//

    }
}
