Assignment Management System

An app for students to manage and track their current, past, and future assignments.

features:
- create, read, update, delete assignments
- search for assignment by title or list all incompleted assignments
- console support and web api support
- unit tested and reliable architecture
- log events for tracing history

Setup
- clone repo
- open AssignmentManagementSystem.sln in Visual Studio
- run AssignmentManagement.Console, AssignmentManagement.Api, or run tests via test explorer

Running Tests
- open AssignmentManagementSystem.sln in Visual Studio
- find “Test” in top toolbar -> select “Run All Tests
- analyze results to find areas that need more coverage

Architecture
- AssignmentManagement.Core contains the core logic for managing Assignments, such as interfaces, services, and the data model for Assignments
- AssignmentManagement.Api is an exposed api for managing assignments remotely. Extension idea: It also allows you to design a custom UI, or create multiple variations that work with the same core logic. This can be useful for students with accessibility needs, or for multi-platform and multi-device support.
- AssignmentManagement.UI is a console based desktop app that allows you to manage assignments from command line. This can be useful for systems that are low on resources or for debugging.
- AssignmentManagement.Tests is the testing suite which covers core logic.
- AssignmentManagement.ApiTests is the testing suite specifically for the Api, to ensure that Api calls run reliably.

Contribute
- fork this repo and submit PR
- include unit tests and documentation of changes

Maintenance Plan
- review code monthly for complexity, cohesion, and readability/clarity
- issues are to be addressed within 2 weeks
- XML comments are made to document new public methods and services
- unit test coverage maintained at 80%+
- touch base with stakeholders and gather feedback 

Developer Notes
- The Assignment management app is designed to assist students of any course in tracking, planning, and coordinating assignments. Personally, this project is the biggest one I have worked with, so it has been an incredible learning experience for me. I hope to pass this enthusiasm for learning to the students who use the application and provide them with tools for success.
