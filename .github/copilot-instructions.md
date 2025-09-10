# GitHub Copilot Instruction File for Drawbridges (Continued) Mod

## Mod Overview and Purpose
The "Drawbridges (Continued)" mod for RimWorld adds functional drawbridges in various sizes into the game. These drawbridges enhance gameplay by allowing different types of vehicles and pawns to interact with the bridges based on their state. The primary purpose of the mod is to offer a dynamic infrastructure solution for managing traffic and resources in player colonies.

## Key Features and Systems
- **Drawbridge Functionality**: The mod introduces drawbridges that can be raised or lowered, affecting the pathing of vehicles and pawns. While raised, the bridges allow sea vehicles to pass through, and when lowered, they enable ground vehicles and pawns to cross.
- **Size Variations**: The bridges come in three distinct sizes (2x6, 3x6, and 5x6) to accommodate different logistical needs in the game.
- **Integration with Pipelines and Power**: Drawbridges are capable of carrying pipelines and power, needing underground versions for optimal integration.
- **Resource and Power Management**: Further developments aim to introduce realistic resource and power costs associated with constructing and maintaining these bridges.

## Coding Patterns and Conventions
- **Class Definition**: Each class within the mod is prefixed with access modifiers like `public` and extensive use of proper naming conventions to ensure clarity.
- **Method Structure**: Methods are organized with a strong emphasis on single responsibility, making the codebase easier to maintain and extend.
- **XML Documentation**: Use of XML documentation for classes and methods is encouraged to enhance code readability and API documentation.

## XML Integration
- Drawbridge configurations, including sizes and resource/power costs, are likely defined in XML files. The XML structure should reflect logical grouping and contain sufficient tags for Copilot to understand data dependencies and structures.
- Utilize XML patching methods to apply changes without altering the base game files, ensuring compatibility with other mods.

## Harmony Patching
- **Patch Methods**: Implement Harmony to modify or extend the original game methods without directly altering them. This approach helps in maintaining mod compatibility across game updates.
- **Patch Placement**: Strategic placement of prefix, postfix, and transpiler methods to ensure minimal intrusion while ensuring intended behavior of drawbridges.

## Suggestions for Copilot
- Implement suggestions related to optimal data structure usage within C# for handling bridge state and interaction logic.
- Provide code snippets or suggestions to refine Harmony patching methods and improve bridge interaction dynamics.
- Assist with troubleshooting pathfinding issues for both sea and ground vehicles and suggest debugging strategies for fixing compatibility issues with game updates.
- Facilitate the creation of new features like custom resource and power cost algorithms for the drawbridges.
- Enhance the XML integration process by generating schema or template structures for new drawbridge types or configurations.

By adhering to these instructions, contributors and Copilot can collaboratively improve and maintain the "Drawbridges (Continued)" mod, ensuring a robust and enjoyable gameplay experience.
