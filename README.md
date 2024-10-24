## Practical task repository for Clean Code Advanced Final Task

### General Task Guidelines

* Please fork the repository to your EPAM Git/Gitbud account and make corrections per the principles/topics discussed so far in this course .
* A task/subtopic will be considered as complete after all the review comments are incorporated from the below mentioned review process.
* **thirdparty folder** - while making changes, any files contained inside the "thirdparty" folder must not be changed. **Do not modify 'thirdparty' components.** These files are meant to provide context to the task and are not part of it. folder - while making changes, any files contained inside the "thirdparty" folder must not be changed. Do not modify 'thirdparty' components. These files are meant to provide context to the task and are not part of it.
* Some tasks have unit tests implemented, if they exist, they are comprehensive and we are free to change production code in any way until tests are green. You may add more tests if you feel appropriate but this is not expected.
* On top of what was written, the code has some logical issues that need to be found.
---

## The story

There is a movie studio and its goal is to produce the movie. In that several aspects are involved. Our goal is here to create the accounting system for the movie production. Below is the process

Main module of the project is _MovieStudio_, so it has a public method _CreateMovie_. The process of making a movie consists of:
1. Initially a movie concept is planned with certain genre  (like action, drama, thriller etc) with some rough budget. (movie name, rough production schedule will be there at this stage)
2. Hire some initial staff (like recruiter, accountant) who can later hire additional staff.
3. Now a feasibility study will be done if this movie can really be started. Here  criteria is that whoever (service/person) doing the feasibility will estimate the budget and compare it against the initial budget and if it is under budget, the movie can be realized and production can be started. (otherwise end of story)
4. Assuming that movie can be realized, we need to hire the staff who can actually work on the movie (like technicians and actors).
5. Now actual movie production (shooting) is started and schedule is in place. Here in the schedule, it is assumed that all people will be productive all days (where as in reality production/shooting may not happen on many days because of various reasons and that can delay the movie production.)
6. Workers get paid at the end of the day irrespective of their output on that day.
7. On any particular day (at the end), after paying the salaries to workers, if the cumulative budget spent on the movie exceeds the estimated budget, movie is decided as failure and further activity is stopped.
8. If we reach the schedule end date and if the spent budget is still under limit, then movie can be treated as success.
9. Now since the movie is completed, we print the movie statistics
10. Now at the end, we add the movie to archives collection.

**NB**\
We use some random values to decide if a worker can be productive/nonproductive on a particular day.

### Clean Design Violations

Try to follow object oriented design ( i.e. co-locate data and behavior). Do not try to mix objects and data structures.
We believe the task has SRP, OCP, ISP and DI violations and you can identify and fix them.
Please use appropriate abstractions wherever required. We also believe too much of abstraction can become complex.

**NB**:
Please remember that there is no perfect design, it's all about the tradeoffs based on the context.
