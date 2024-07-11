# Interactive Maze Solver

## Overview
The Interactive Maze Solver is a WPF application that allows users to solve mazes using different algorithms. The application provides predefined mazes and allows users to create their own custom mazes. The solution path is animated for better visualization, and start and end points are marked with images.

## Features
- **Predefined Mazes**: Select from multiple predefined mazes.
- **Custom Maze Creation**: Users can create their own mazes.
- **Algorithms**: Solve mazes using Breadth-First Search (BFS) or Depth-First Search (DFS).
- **Animated Path Visualization**: Watch the solution path being drawn block by block.
- **Image Embedding**: Start and end points are marked with custom images.
- **Reset Functionality**: Clear the solution path to start over.

## Technologies Used
- C#
- WPF (Windows Presentation Foundation)
- XAML

## Getting Started

### Prerequisites
- .NET 
- Visual Studio or any C# IDE

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/salmanhaider14/interactive-maze-solver.git
   ```
2. Open the solution in Visual Studio.
3. Restore any NuGet packages if required.
4. Build the solution.

### Usage
1. Run the application.
2. Choose a predefined maze from the `Maze` dropdown.
3. Select the algorithm (DFS or BFS) from the `Algorithm` dropdown.
4. Click `Solve` to solve the maze and visualize the path.
5. Click `Reset` to clear the visualization.
6. Toggle the `Custom Maze` checkbox to switch between predefined and custom maze modes.

## Future Enhancements
- **Additional Algorithms**: Implement other pathfinding algorithms like A*.
- **Maze Difficulty Levels**: Add options for different difficulty levels for custom mazes.
- **More Visual Customizations**: Allow users to customize the appearance of the maze and path.

## Contributing
1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature-branch
   ```
3. Make your changes and commit them:
   ```bash
   git commit -m "Description of changes"
   ```
4. Push to the branch:
   ```bash
   git push origin feature-branch
   ```
5. Create a pull request.

## License
This project is licensed under the MIT License.

## Contact
For any questions or suggestions, feel free to reach out at salmanpatrick5@gmail.com.

---
