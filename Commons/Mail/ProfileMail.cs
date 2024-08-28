//using Commons.Identity;
//using Commons.Models;
//using Microsoft.AspNetCore.Identity;

//namespace Commons.Mail
//{
//    public class ProfileMail<TUser, TProfile, TWorkSpace> : Relational 
//        where TUser : IdentityUser
//        where TProfile : CommonsProfile<TUser, TWorkSpace>
//        where TWorkSpace : CommonsWorkSpace
//    {
//        public virtual Mail<TUser, TProfile, TWorkSpace> Mail { get; set; }

//        public bool IsRead { get; set; } = false;

//        public virtual TProfile Recipent { get; set; }

//        public bool IsInTrash()
//        {
//            return !IsActive();
//        }
//    }
    
//}
