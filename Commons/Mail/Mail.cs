//using System.Collections.Generic;
//using System.Linq;
//using Commons.Identity;
//using Commons.Models;
//using Microsoft.AspNetCore.Identity;

//namespace Commons.Mail
//{
//    public class Mail<TUser, TProfile, TWorkSpace> : Documental 
//        where TProfile : CommonsProfile<TWorkSpace> 
//        where TUser : IdentityUser
//        where TWorkSpace : CommonsWorkSpace
//    {
//        public string Subject { set; get; }
//        public string MessageBody { get; set; }
//        public virtual TProfile Sender { get; set; }
//        public virtual Mail<TUser, TProfile, TWorkSpace> ParentMail { get; set; }

//        public virtual List<ProfileMail<TUser, TProfile, TWorkSpace>> Recipients { get; set; } = new List<ProfileMail<TUser, TProfile, TWorkSpace>>();

//        public void AddRecipient(TProfile profile)
//        {
//            if (Recipients.Any(x => x.IsActive() && x.Recipent == profile)) return;
//            Recipients.Add(new ProfileMail<TUser, TProfile, TWorkSpace>()
//            {
//                Recipent = profile,
//                IsRead = false
//            });
//        }

//        public void RemoveRecipient(TProfile profile)
//        {
//            var profileMail = Recipients.Find(x => x.IsActive() && x.Recipent == profile);

//            profileMail?.Delete();
//        }

//        public List<Mail<TUser, TProfile, TWorkSpace>> GetConversation()
//        {
//            List<Mail<TUser, TProfile, TWorkSpace>> mailList = new List<Mail<TUser, TProfile, TWorkSpace>>();
//            Mail<TUser, TProfile, TWorkSpace> index = this;
//            mailList.Add(index);

//            while (index.ParentMail != null)
//            {
//                mailList.Add(index.ParentMail);
//                index = index.ParentMail;
//            }

//            return mailList;
//        }
        
//    }
//}
