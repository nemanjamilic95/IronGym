using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EfDataAccess;

namespace Iron.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new IronContext();


            context.Roles.Add(new Role
            {
                Name = "Admin"
            });
            context.Roles.Add(new Role
            {
                Name = "User"
            });
            context.Roles.Add(new Role
            {
                Name = "Trainer"
            });
            context.SaveChanges();



            context.Users.Add(new User
            {
                Username = "admin",
                Password = "papiriC826",
                FirstName = "Admin",
                LastName = "Admin",
                Avatar = "avatar.png",
                Email = "nemanja.milic.169.14@ict.edu.rs",
                RoleId = 1
            });
            context.Users.Add(new User
            {
                Username = "user",
                Password = "user123",
                FirstName = "User",
                LastName = "User",
                Avatar = "avatar.png",
                Email = "user@user.com",
                RoleId = 2
            });
            context.Users.Add(new User
            {
                Username = "trainer",
                Password = "trainer123",
                FirstName = "Trainer",
                LastName = "Trainer",
                Avatar = "avatar.png",
                Email = "nmilic7643@gmail.com",
                RoleId = 3
            });

            context.SaveChanges();


            
            context.Posts.Add(new Post
            {
                Heading= "FST-7 Program",
                Text= "The third type of volume training program that's catching on rather rapidly is the FST-7 Training Program. This training program doesn't specifically lay out all the exercises you need to perform in a given session nor does it specifically state that you must divide the body up into a certain protocol (upper body and lower body or chest/back, legs and shoulder for example)",
                Picture= "img_1.jpg",
                UserId=3
                
            });
            context.Posts.Add(new Post
            {
                Heading = "The 5 X 5 Program",
                Text = "The set-up of this program is to perform three main exercises that target the main muscle groups in the body (both lower and upper body in the same workout), performing five sets of five repetitions.Example:1.Barbell Squat(5 sets, 5 reps)2.Barbell Bench Press(5 sets, 5 reps)3.Bent Over Barbell Row(5 sets, 5 reps)4.Pullups(5 sets, 5 reps)5.Sit - up(5 sets, 5 reps)",
                Picture = "5x5.png",
                UserId = 3

            });
            context.Posts.Add(new Post
            {
                Heading = "Volume Training",
                Text = "For this workout protocol, you are to select one compound exercise for each muscle group and hit it hard with ten sets of ten reps. Once those have been completed, then you can add a few isolation exercises if you wish but bring them down to only 2-3 sets of 10-15 reps.Aim to keep up the pace of the workout by keeping your rest to 60 - 90 seconds.Take one day off between workouts and have the full weekend for solid recuperation.",
                Picture = "img_3.png",
                UserId = 3

            });
            context.Posts.Add(new Post
            {
                Heading = "A Four-Week Routine",
                Text = "In each of the four weeks of this 28-day plan you will train your chest and back twice. Sound like a lot? But in this plan,doubling up each week on chest and back exercises – and therefore also working your biceps and triceps twice a week, once directly and once indirectly – will provide all the stimulus your body needs to get bigger in less time.",
                Picture = "4weeks.jpg",
                UserId = 3

            });
            context.SaveChanges();

            context.Programs.Add(new Domain.Program
            {
                Heading= "Muscle Building",
                Text= "The conventional wisdom says if you're trying to gain muscle, you need to take in one gram of protein per pound of bodyweight.By that logic,a 160 - pound man should consume around 160 grams of protein a day(the amount he'd get from an 8-ounce chicken breast, 1 cup of cottage cheese, a roast-beef sandwich, two eggs, a glass of milk, and 2 ounces of peanuts.) If you don't eat meat for ethical or religious reasons,don't worry — you can count on other sources, too.",
                Picture= "img_1.jpg"
            });
            context.Programs.Add(new Domain.Program
            {
                Heading = "Fat Loss",
                Text = "Too many people view fat loss like it is a secret VIP party that requires you to do or say the right thing to get invited.Fat loss is a biological process that does not need to be shrouded in mystery. It is not as easy as some of the gimmicks would have you believe but an understanding of the processes that lead to fat loss will allow you to make the correct decisions to get you where you want to be.Here are no quick fixes. This is only for those that are willing to put in the work and reap the benefits of that work.",
                Picture = "img_2.jpg"
            });
            context.Programs.Add(new Domain.Program
            {
                Heading = "Body Weight",
                Text = "Bodyweight exercises are strength training exercises that use the individual's own weight to provide resistance against gravity. Bodyweight exercises can enhance a range of biomotor abilities including strength, power, endurance, speed, flexibility, coordination and balance. This type of strength training has grown in popularity for both recreational and professional athletes. Bodyweight training utilises simple abilities such as pushing, pulling, squatting, bending, twisting and balancing. Movements such as the push-up, the pull - up,and the sit - up are some of the most common bodyweight exercises.",
                Picture = "img_3.jpg"
            });
            context.SaveChanges();

            context.Comments.Add(new Comment
            {
                Text= "This program are so good,  and very similar to my post about volume training. Extremely useful. Well done,dude.",
                PostId=2,
                UserId=2
            });
            context.Comments.Add(new Comment
            {
                Text = "Yeah, I have try both of them, and I must say that I'm so satisfied.",
                PostId =2,
                UserId =1
            });
            context.Comments.Add(new Comment
            {
                Text = "I am very pleased that this program positively affects those who use it. I'm also training according to this program.",
                PostId =2,
                UserId =3
            });
            context.SaveChanges();


            context.Likes.Add(new Like
            {
                PostId=2,
                UserId=1
            });
            context.Likes.Add(new Like
            {
                PostId = 2,
                UserId = 2
            });
            context.Likes.Add(new Like
            {
                PostId = 2,
                UserId = 3
            });
            context.Likes.Add(new Like
            {
                PostId = 1,
                UserId = 1
            });
            context.Likes.Add(new Like
            {
                PostId = 1,
                UserId = 2
            });
            context.Likes.Add(new Like
            {
                PostId = 1,
                UserId = 3
            });
            context.SaveChanges();
        }
    }
}
