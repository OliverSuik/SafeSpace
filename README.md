## SafeSpace

An application that allows students to book seats in classrooms during the COVID-19 pandemic and helps lecturers keep track of attendace and possible virus exposures.

## Project Status

This project is currently in development. All the views, functionalities, authentication and authorization have been implemented. Deployment using MS Azure and testing is the next step.

## Project Screen Shot(s)
Booking a seat.
![Booking a seat.](https://user-images.githubusercontent.com/44434676/111087098-5b302500-8528-11eb-98a6-86ca8567997f.PNG)
Creating a seating plan.
![Creating a seating plan.](https://user-images.githubusercontent.com/44434676/111087101-5c615200-8528-11eb-9883-909a318fa686.PNG)
Creating a session.
![Creating a session.](https://user-images.githubusercontent.com/44434676/111087102-5c615200-8528-11eb-8d17-ff4a337db5ea.PNG)

## Installation and Setup Instructions

## Reflection

  - What was the context for this project? (ie: was this a side project? was this for Turing? was this for an experiment?)
  - What did you set out to build?
  - Why was this project challenging and therefore a really good learning experience?
  - What were some unexpected obstacles?
  - What tools did you use to implement this project?
      - This might seem obvious because you are IN this codebase, but to all other humans now is the time to talk about why you chose webpack instead of create react app, or D3, or vanilla JS instead of a framework etc. Brag about your choices and justify them here.  

#### Example:  

This was a 3 week long project built during my third module at Turing School of Software and Design. Project goals included using technologies learned up until this point and familiarizing myself with documentation for new features.  

Originally I wanted to build an application that allowed users to pull data from the Twitter API based on what they were interested in, such as 'most tagged users'. I started this process by using the `create-react-app` boilerplate, then adding `react-router-4.0` and `redux`.  

One of the main challenges I ran into was Authentication. This lead me to spend a few days on a research spike into OAuth, Auth0, and two-factor authentication using Firebase or other third parties. Due to project time constraints, I had to table authentication and focus more on data visualization from parts of the API that weren't restricted to authenticated users.

At the end of the day, the technologies implemented in this project are React, React-Router 4.0, Redux, LoDash, D3, and a significant amount of VanillaJS, JSX, and CSS. I chose to use the `create-react-app` boilerplate to minimize initial setup and invest more time in diving into weird technological rabbit holes. In the next iteration I plan on handrolling a `webpack.config.js` file to more fully understand the build process.
