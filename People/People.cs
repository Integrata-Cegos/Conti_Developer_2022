namespace People{
    public enum Gender{
        Male, Female, Diverse
    }
    public class Person{

        public static int PeopleCounter {get; private set;} = 0;
        private string? _Lastname;
        public string Lastname{
            get {
                return _Lastname!.ToUpper();
            }
            set {
                if (value.Length > 0 && value.Length < 32){
                    _Lastname = value;
                }
            }
        }

        public string Name{
            get {
                return Firstname + " " + Lastname;
            }
        }

        public string Firstname{get;}
        public int Height{get;}
        public Gender Gender{get;set;}

        public string SayHello(){
            //Innerhalb einer Methode existiert die implizite Variable "this"
            //sawitzki.SayHello -> this = sawitzki
            return "Hello, my name is " + this.Lastname;
            //return "Hello, my name is " + Lastname;//Der Compiler ergänzt hier this.
        }
        public Person(string Lastname, string Firstname, int Height, Gender Gender){
            this.Lastname = Lastname;
            this.Firstname = Firstname;
            this.Height = Height;
            this.Gender = Gender;
            Person.PeopleCounter++;
        }
        private Person? partner;

        public string Marry(Person? partner){
            if (partner == null){
                return "marry failed: partner was null";
            }
            if (partner == this){
                return "marry failed: you cannot marry yourself";
            }
            if (partner.partner != null){
                return "marry failed: partner is married";
            }
            if (this.partner != null){
                return "marry failed: you are married";
            }
            this.partner = partner;
            partner.partner = this;
            return "marry OK";
        }

        public string Divorce(){
            if (this.partner == null){
                return "Divorce failed: you are not married!";
            }
            this.partner.partner = null;
            this.partner = null;
            return "divorce OK";
        }
    }
}