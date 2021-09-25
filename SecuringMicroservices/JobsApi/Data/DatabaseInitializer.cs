using JobsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsApi.Data
{
    public class DatabaseInitializer
    {

        public static void Initialize(JobsContext context)
        {

            context.Database.EnsureCreated();
            if(context.Jobs.Any())
            {
                return; //DB has already been seeded
            }
            var jobs = new Job[]
           {
                 new Job { Title = "Senior Software Engineer", Description = "We are seeking a Senior Software Engineer to implement ease-of-use functionality for our integrated IT Risk Management Platform built on .Net, SQL and Angular JS.", Company = "HyperSec", PostedDate = DateTime.UtcNow, Location = "Toronto" },
                 new Job { Title = "Developer (.Net)", Description = "Design, implement, debug web-based applications using the appropriate tools and adhering to our coding standards Review project requirements, assess and estimate the necessary time-to-completion Contribute to and lead architecture and design activities Create unit test plans and scenarios for development unit testing Interact with other development teams to carry out code reviews and to ensure a consistent approach to software development Deploy all integration artifacts to a testing and production environment Assist other developers in resolving software development issues Perform additional duties as needed", Company = "Hyperbol Securities", PostedDate = DateTime.Now, Location = "Seattle" },
                 new Job {Title="C#/.NET Developer", Description ="Help schools and parents in a meaningful way by using your talent as a developer to create real world solutions for their needs. We are looking for a strong C# / .NET Developer to join our product development team responsible for designing then developing new products, as well as improving our current suite of desktop, web, and mobile applications. Some of the technologies we work with are ASP.NET Core / React / Redux, WPF using the MVVM pattern, SOAP / REST based services, and MS-SQL.",  Company= "Image Soft", PostedDate=DateTime.UtcNow, Location ="Toronto" },
                 new Job { Title="Full-Stack Web Developer (ASP.NET Core / React.js / C#)", Description= "Help schools and parents in a meaningful way by using your talent as a developer to create real world solutions for their needs. We are looking for a strong Full-Stack Web Developer to join our product development team responsible for designing then developing new products, as well as improving our current suite of application using modern web technologies such as ASP.NET Core 2.0 / Node.js / React / Redux / Bootstrap using C# and TypeScript.  Our intelligent software manages student fees, bills parents, collect payments, synchronizes and transforms data from many different sources, and includes reporting and visualization features. Contribute to project requirements, system architecture, and brainstorm product ideas. Then, build and execute on these as part of a Scrum team using a high-end computer and large screens!", Company ="Progressive Software  Revolution", PostedDate= DateTime.UtcNow, Location ="Vancouver" },
                 new Job {Title ="Snr full stack C#.NET Developer", Description ="We''re currently looking for a talented Senior Back-End .Net Developer to join our team on a permanent full time position. The ideal candidate will have the opportunity to work for one of the highest grossing E-Tailers in North America.  What you''ll do: Maintain existing CRM, and Web-based applications in VB.NET/ASP.NET. Create user-friendly and process-efficient interfaces and tools for internal staff to access data relevant to our business. Interact with staff to track down bugs, feature requests, and potential improvements to internal applications Assist in IT related activities.", Company ="The Specials Software",   PostedDate= DateTime.UtcNow, Location ="Berlin" },
                 new Job { Title ="Software Developer/Consultant", Description="Collaborate in small teams to design, build, and deploy quality software solutions for our enterprise customers Help shape our long-term technical roadmap as we scale our infrastructure and build new products Solve complex problems related to development and provide accurate estimates and scope for team deliverables Work independently and be able to effectively communicate verbally and in writing with both our customers and internal teams Perform other job-related duties as assigned, we are a small and growing organization where we are all continuously improving our daily activities to be more productive Needs to be available for occasional travel (BC, Alberta, Washington) Needs to be able to multitask, we are looking for a keen individual ready to learn and adapt to a high paced customer facing position", Company= "ABC Space Group", PostedDate=DateTime.Now,  Location="London" }
           };
            context.Jobs.AddRange(jobs);
            context.SaveChanges();
        }
    }
}
