using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create the videos
        Video video1 = new Video("Learning C#", "Ekemini", 600);
        Video video2 = new Video("Introduction to Python", "John", 480);
        Video video3 = new Video("How to Code", "Mary", 720);
        Video video4 = new Video("Programming Basics", "David", 540);

        // Add comments to video 1
        video1.Comments.Add(new Comment("David", "This video was very helpful."));
        video1.Comments.Add(new Comment("Sarah", "I learned a lot from this."));
        video1.Comments.Add(new Comment("Mike", "Great explanation!"));
        video1.Comments.Add(new Comment("Grace", "I really enjoyed this video."));

        // Add comments to video 2
        video2.Comments.Add(new Comment("James", "Python is interesting."));
        video2.Comments.Add(new Comment("Linda", "Thanks for sharing this."));
        video2.Comments.Add(new Comment("Peter", "Very useful video."));
        video2.Comments.Add(new Comment("Anna", "I learned something new."));

        // Add comments to video 3
        video3.Comments.Add(new Comment("Daniel", "I enjoyed this video."));
        video3.Comments.Add(new Comment("Grace", "This helped me understand coding."));
        video3.Comments.Add(new Comment("Samuel", "Excellent work!"));
        video3.Comments.Add(new Comment("Rachel", "Very clear explanation."));

        // Add comments to video 4
        video4.Comments.Add(new Comment("Michael", "This was a great lesson."));
        video4.Comments.Add(new Comment("Joseph", "I really liked this video."));
        video4.Comments.Add(new Comment("Emily", "Very informative."));
        video4.Comments.Add(new Comment("Thomas", "Thank you for teaching this."));

        // Put all videos into a list
        List<Video> videos = new List<Video>();
        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);
        videos.Add(video4);

        // Display each video and its comments
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of comments: {video.GetNumberOfComments()}");

            Console.WriteLine("Comments:");

            foreach (Comment comment in video.Comments)
            {
                Console.WriteLine($"{comment.Name}: {comment.Text}");
            }

            Console.WriteLine();
        }
    }
}