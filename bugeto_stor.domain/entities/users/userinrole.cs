namespace bugeto_stor.domain.entities.users
{
    public partial class user
    {
        public static object identity;
        public readonly object Id;

        public class userinrole
        {
            public bool isremoved;

            public long id { get; set; }
            public virtual user user { get; set; }
            public long userid { get; set; }


            public virtual role role { get; set; }
            public long roleid { get; set; }
        }

        public class removetime
        {
        }

        public class RemoveTime
        {
        }
    }
}
