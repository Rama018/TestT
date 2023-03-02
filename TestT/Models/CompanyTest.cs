using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
using MessagePack;
using Newtonsoft.Json.Serialization;

namespace TestT.Models
{


    /// <summary>
    /// Custome validation ++++++++++++++++++++++++++++++++::::::::::+++++++++++++++++++++++++++++++++++++++++_____________::::::::::::
    /// </summary>
    public class CompanyTest
    {
        [ValidateNever]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter The Name")]
        [DisplayName("Company Name")]

        [NameValidation(ErrorMessage = "Name is already there")]
        public string Name { get; set; }
        public string Description { get; set; }


        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                              @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                              @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                              ErrorMessage = "Email is not valid")]
        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression("(http|ftp|https):\\/\\/[\\w\\-_]+(\\.[\\w\\-_]+)+([\\w\\-\\.,@?^=%&amp;:/~\\+#]*[\\w\\-\\@?^=%&amp;/~\\+#])?", ErrorMessage = "format must be for example : https://www.facebook.com/")]

        public string WebSite { get; set; }


        public static List<CompanyTest> GetAll()
        {
            List<CompanyTest> store = null;
            using (var Context2 = new TestContext())
            {
                store = Context2.test.ToList<CompanyTest>();
            }
            return store;
        }

        public static void Create(CompanyTest CO)
        {
            TestContext Context = new TestContext();
            Context.test.Add(CO);
            Context.SaveChanges();

        }
        public static CompanyTest getID(int id)
        {

            CompanyTest str = null;
            using (var Context2 = new TestContext())
            {

                str = Context2.test.Find(id);
            }
            return str == null ? null : str;
        }
        public static void Update(CompanyTest com)
        {
            using (var Context2 = new TestContext())
            {

                Context2.test.Update(com);
                Context2.SaveChanges();
            }
        }
        public static void Delete(CompanyTest com)
        {
            using (var Context2 = new TestContext())
            {

                Context2.test.Remove(com);
                Context2.SaveChanges();
            }


        }

        public static bool Create2(CompanyTest obj)
        {

            TestContext context = new TestContext();
            context.test.Add(obj);
            context.SaveChanges();
            context.Dispose();
            return obj == null ? false : true;
        }

        public static CompanyTest UpdateID2(int id)
        {
            CompanyTest str = null;
            CompanyTest obj = new CompanyTest();
            obj.Id = id;
            using (var Context2 = new TestContext())
            {

                str = Context2.test.FirstOrDefault(y => y.Id == id);
                if (str != null)
                {
                    Context2.test.Update(str);

                   Context2.SaveChanges();
                }
                
            }
            return str == null ? null : str;
        }
        public static bool DeleteByID(int ID)
        {
            CompanyTest str = null;
            CompanyTest obj = new CompanyTest();
            obj.Id = ID;
            using (var Context2 = new TestContext())
            {
                str = Context2.test.FirstOrDefault(y => y.Id == ID);
                Context2.test.Remove(str);
                Context2.SaveChanges();

            }
            if (str != null)
                return true;
            else
                return false;
        }

        public static List<CompanyTest> Allpagin(int PNum , int PSize)
        {
            List<CompanyTest> store = null;
            using (var Context2 = new TestContext())
            {

                store = Context2.test.Skip((PNum-1)*PSize).Take(PSize).ToList();
            }
            return store;
        }

        public class NameValidation : ValidationAttribute
        {
            public string GetErrorMessage() =>
                "Please Enter a new company name ... is already there";

            protected override ValidationResult? IsValid
                (object? value, ValidationContext validationContext)
            {


                CompanyTest? Co = null;
                using (var Context2 = new TestContext())
                {

                    //value.tostring() لأنو اجاني ك أوبجكت 
                    Co = Context2.test.FirstOrDefault(y => y.Name == value.ToString());


                }


                return Co == null ? ValidationResult.Success : new ValidationResult(GetErrorMessage());
            }


        }
    }
}

