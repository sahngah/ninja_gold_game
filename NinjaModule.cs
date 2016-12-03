using Nancy;
using System;
using System.Collections.Generic;
namespace NinjaGold
{
    public class NinjaModule : NancyModule
    {
        public NinjaModule()
        {
            Get("/", args => 
            {
                if (Session["score"] == null)
                {
                    ViewBag.zero_score = true;
                } 
                if (Session["list"] == null)
                {
                    Session["list"] = new List<string>();
                } 
                ViewBag.score = Session["score"];
                List<string> list = (List<string>)Session["list"];
                return View["home", list];
            });
            Post("process_money", args => 
            {  
                string place = Request.Form.building;
                int gold = 0;
                DateTime now = DateTime.Now;
                // string now = date.ToString("yyyy/MM/dd, h:mm tt");

                List<string> action_list = new List<string>();
                switch(place)
                {
                    case "farm":
                        gold = new Random().Next(10, 21);
                        int score = Convert.ToInt32(Session["score"]);
                        score += gold;
                        Session["score"] = score;
                        break;
                    case "cave":
                        gold = new Random().Next(5, 11);
                        score = Convert.ToInt32(Session["score"]);
                        score += gold;
                        Session["score"] = score;
                        break;
                    case "house":
                        gold = new Random().Next(2,6);
                        score = Convert.ToInt32(Session["score"]);
                        score += gold; 
                        Session["score"] = score;
                        break;
                    case "casino":
                        gold = new Random().Next(-50, 51);
                        score = Convert.ToInt32(Session["score"]);
                        score += gold;  
                        Session["score"] = score;
                        break;
                }
                string action;
                if (gold >= 0)
                {
                    action = $"You earned {gold} from the {place}! {now}";
                }
                else
                {
                    action = $"Enter a Casino and lost {gold} golds...Ouch. {now}";
                }
                action_list = (List<string>)Session["list"];
                action_list.Add(action);
                Session["list"] = action_list;
                return Response.AsRedirect("/");
            });
        }
    }
}