# Media Player Application

Assignment for Integrify Academy:
Build a media player application that demonstrates advanced topics in C# programming, including SOLID principles, Clean architecture, Factory pattern, Singleton pattern, Observer pattern, object lifetime, and thread safety.

## Shotscreens results

- MediaFiles and Subscribe features<br>
  ![image](https://github.com/ericpastor/part0/assets/110885492/8f800990-d132-4fd3-8f72-028f103ff36f)
  ![image](https://github.com/ericpastor/part0/assets/110885492/1d10c4b9-6d8d-48a8-a4a2-63789cd3af69)
  <br>

- Adding/updating customers<br>
  ![image](https://github.com/ericpastor/part0/assets/110885492/c1368fc2-eb27-41c4-a9c0-da88b749ae92)
  ![image](https://github.com/ericpastor/part0/assets/110885492/770e7637-d9d3-4e0b-89f2-bf53a426444f)
  <br>

- Customer adding/removing files<br>
  ![image](https://github.com/ericpastor/part0/assets/110885492/32748f1d-8176-4131-a5a0-61085661af77)
  ![image](https://github.com/ericpastor/part0/assets/110885492/c1d5e0b6-6b02-48a2-98c9-c5b203cf65fb)
  <br>

- Customer playing MediaFiles<br>
  ![image](https://github.com/ericpastor/part0/assets/110885492/ab3c7963-99eb-4437-91b9-d909d235eb8f)
  ![image](https://github.com/ericpastor/part0/assets/110885492/2ed12a46-d932-4ddc-9f81-2ac331132d19)
  <br>

## Tests results

![Alt text](image.png)

## Basic features

The media player application is a robust software which contains collections of different media files and users. Each user could create their own playtracks and perform other actions on the media file in their playtracks. One user can have multiple playtracks. Application should not have identical users.

- Only Admins of the application should be able to add, remove, update, delete all the files and users in the application.
- Users should be able to manage their playtracks, including adding, removing, play, pause, stop the media files.
- Media files can be further adjusted while playing:
  - Videos can change volume, brightness (can simply use `int` or `string`)
  - Audios can change volume, sound effect (can simply use `int` or `string`)
- Handle potential errors and exceptions gracefully, providing meaningful error messages to the user.

## Advanced (optional) features

- Display the current status of the certain playtracks (such as the total media files and playtrack owner) and status of certain media file in that playtrack, such as information, the playing status, current position (if on playing) and duration.
- Design a user-friendly command-line interface that allows users to navigate and interact with the application. Provide clear instructions and feedback to guide users through different operations.

## Requirement:

- Design a solid and clean architecture for the media player application.
