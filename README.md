# TaskTruckerCLI

**TaskTruckerCLI** is a command-line application I developed in **.NET 8** to efficiently manage tasks. It allows adding, updating, deleting, and listing tasks, as well as marking their status as **todo**, **in-progress**, or **done**.

---

## 🔗 Project URL

Project Source: **https://roadmap.sh/projects/task-tracker**

---

## Features

* Add, update, and delete tasks
* Mark tasks as **in-progress** or **done**
* List all tasks or filter by status
* Data persistence in a **JSON** file in the project directory

---

## Task Structure

Each task has the following properties:

* `id`: unique identifier
* `description`: task description
* `status`: `todo`, `in-progress`, `done`
* `createdAt`: creation date and time
* `updatedAt`: last update date and time

---

## Project Steps

1. **Create the Project**

```bash
git clone https://github.com/DaltonLuis/TaskTrackerCLI.git
cd TaskTruckerCLI
dotnet run
```

2. **Set Up File Structure**

* Create a JSON file (`tasks.json`) in the project directory to store tasks.

3. **Implement Features**

* Add tasks: `add`
* Update tasks: `update`
* Delete tasks: `delete`
* Mark status: `mark-in-progress` / `mark-done`
* List tasks: `list` (optional: `todo`, `in-progress`, `done`)

4. **Handle Inputs and Errors**

* Validate IDs and command-line arguments
* Create JSON if it doesn't exist
* Show clear messages for success or error

5. **Test and Finalize**

* Test each command individually
* Verify persistence in the JSON file
* Clean up code and add comments

---

## Usage Examples

```bash
# Add a task
task-cli: add "Buy groceries"

# Update a task
task-cli: update 1 "Buy groceries and cook dinner"

# Mark a task
task-cli: mark-in-progress 1
task-cli: mark-done 1

# List tasks
task-cli: list
task-cli: list done
task-cli: list todo
task-cli: list in-progress
```
