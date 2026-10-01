using UnityEngine;

namespace AdamKacem.RoomsGeneration
{
    public class wallPlacer : MonoBehaviour
    {

        [Tooltip("Full wall piece: exactly 4 units long. Rules for all wall pieces: pivot at the bottom, centered along the length; at rotation 0 the wall runs along X and its room side faces +Z (the blue arrow). The generator overrides the root rotation, so rotate the model inside a parent object if needed.")]
        public GameObject wall;

        [Tooltip("Wall piece with a doorway: same size and pivot as the full wall (4 units).")]
        public GameObject doorWall;

        public RoomGrid room;
        [Tooltip("Filler pieces 1, 2 and 3 units long (0.25 / 0.5 / 0.75 of a wall). Only used when the map size is not a multiple of 4. Same pivot rules as the full wall.")]
        public GameObject wall25, wall5, wall75;

        int WALLWIDTH = MapGenerationSettings.WallPieceSize;



        int openTop;
        int openBottom;
        int openLeft;
        int openRight;



        public void Init(RoomGrid room, int openTop, int openBottom, int openRight, int openLeft)
        {
            this.room = room;

            this.openBottom = openBottom;
            this.openLeft = openLeft;
            this.openRight = openRight;
            this.openTop = openTop;

        }




        public void PlaceWalls()
        {







            Vector3 origin = room.origin;
            Quaternion rotation90 = Quaternion.Euler(0f, 90f, 0f);
            Quaternion rotationM90 = Quaternion.Euler(0f, -90f, 0f);
            Quaternion rotation180 = Quaternion.Euler(0f, 180f, 0f);


            Vector3 widthPos = new(1f, 0, -1f);
            Vector3 heightPos = new(-1f, 0, 1f);

            widthPos += origin;
            heightPos += origin;

            Vector3 zTranslator = new Vector3(0,0, room.gridHeight + 2);
            Vector3 xTranslator = new Vector3(room.gridWidth + 2,0, 0);
            //top side
            for (int i = 0; i < ((room.gridWidth+2)/WALLWIDTH); i++)
            {   
                    if (openTop == i )
                    {


                        Instantiate(doorWall , widthPos + zTranslator, rotation180, transform);
                        widthPos += new Vector3(WALLWIDTH, 0, 0);
                        continue;
                    }


                Instantiate(wall, widthPos + zTranslator, rotation180, transform);
                widthPos += new Vector3(WALLWIDTH, 0, 0);

            }
            widthPos = new(1f, 0, -1f);
            widthPos += origin;
            //bottom side
            for (int i = 0; i < ((room.gridWidth + 2) / WALLWIDTH); i++)
            {

                    if (openBottom == i)
                    {

                        Instantiate(doorWall , widthPos, Quaternion.identity, transform);

                        widthPos += new Vector3(WALLWIDTH, 0, 0);
                        continue;
                    }

                Instantiate(wall, widthPos, Quaternion.identity, transform);

                widthPos += new Vector3(WALLWIDTH, 0, 0);

            }

            int widthMod = (room.gridWidth + 2) % WALLWIDTH;

            switch (widthMod)
            {
                case 0:
                    break;
                case 1:
                    Instantiate(wall25, widthPos - new Vector3(1.5f,0,0), Quaternion.identity, transform);
                    Instantiate(wall25, widthPos + zTranslator - new Vector3(1.5f, 0, 0), rotation180, transform);
                    break;
                case 2:
                    Instantiate(wall5, widthPos - new Vector3(1f, 0, 0), Quaternion.identity, transform);
                    Instantiate(wall5, widthPos + zTranslator - new Vector3(1f, 0, 0) , rotation180, transform);
                    break;
                default:
                    Instantiate(wall75, widthPos - new Vector3(0.5f, 0, 0), Quaternion.identity, transform);
                    Instantiate(wall75, widthPos + zTranslator - new Vector3(0.5f, 0, 0), rotation180, transform);
                    break;
            }


            //left side
            for (int i = 0; i < ((room.gridHeight+2)/WALLWIDTH); i++)
            {

                    if (openLeft == i ) {

                        Instantiate(doorWall , heightPos, rotation90, transform);


                        heightPos += new Vector3(0, 0, WALLWIDTH);
                        continue; }


                Instantiate(wall, heightPos, rotation90, transform);

                heightPos += new Vector3(0, 0, WALLWIDTH);

            }
            heightPos = new(-1f, 0, 1f);
            heightPos += origin;
            //right side
            for (int i = 0; i < ((room.gridHeight+2)/WALLWIDTH); i++)
            {

                    if (openRight == i ) {


                        Instantiate(doorWall, heightPos + xTranslator, rotationM90, transform);

                        heightPos += new Vector3(0, 0, WALLWIDTH);
                        continue; }


                Instantiate(wall, heightPos+xTranslator, rotationM90, transform);

                heightPos += new Vector3(0, 0, WALLWIDTH);

            }









            int heightMod = (room.gridHeight + 2) % WALLWIDTH;
            switch (heightMod)
            {
                case 0:
                    break;
                case 1:
                    Instantiate(wall25, heightPos - new Vector3(0, 0, 1.5f), rotation90, transform);
                    Instantiate(wall25, heightPos + xTranslator - new Vector3(0, 0, 1.5f), rotationM90, transform);
                    break;
                case 2:
                    Instantiate(wall5, heightPos - new Vector3(0, 0, 1.5f), rotation90, transform);
                    Instantiate(wall5, heightPos + xTranslator - new Vector3(0, 0, 1.5f), rotationM90, transform);
                    break;
                default:
                    Instantiate(wall75, heightPos - new Vector3(0, 0, 0.5f), rotation90, transform);
                    Instantiate(wall75, heightPos + xTranslator - new Vector3(0, 0, 0.5f), rotationM90, transform);
                    break;
            }






        }


    }
}
