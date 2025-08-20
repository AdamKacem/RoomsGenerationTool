using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class BSPNode
{
    public RectInt area;

    public BSPNode left;
    public BSPNode right;

    public SeededRandom rng;

    int WALLWIDTH = 4;
    public bool isLeaf => left == null && right == null;

    public int topDoor;
    public int bottomDoor;
    public int leftDoor;
    public int rightDoor;

    

    public BSPNode(RectInt area)
    {
        this.area = area;
        this.topDoor = -1;
        this.bottomDoor = -1;
        this.rightDoor = -1;
        this.leftDoor = -1;
    }




    public void SplitWithDoors(int minWallNum, SeededRandom rng, bool allowBigRooms)
    {
        //rl indicates if the child is a right or a left one since children always have unique parents

        

        //this function splits rectangles following wallwidth, in a way that a split will only start from a specific edge of a whole wall piece 

        

        if (!isLeaf) return;
        int widthInWalls = area.width / WALLWIDTH;
        int heightInWalls = area.height / WALLWIDTH;

        bool splitH = rng.Range(0.0f, 1.0f) > 0.5f;
        int maxForSplit = (splitH ? heightInWalls : widthInWalls) - minWallNum;
        if (allowBigRooms)
        {
            if (maxForSplit <= minWallNum) return;
        }
        else
        {
            if (maxForSplit <= minWallNum)
            {
                splitH=!splitH;
                maxForSplit = (splitH ? heightInWalls : widthInWalls) - minWallNum;
                if (maxForSplit <= minWallNum) return;
            }
        }
        
        int maxForDoor = (splitH ? widthInWalls : heightInWalls) - 1 ;


        
        if (splitH)
        {    
            

            
            //choose wallPiece to split from
            int split = rng.Range(minWallNum, maxForSplit);
            //choose wallPiece to become a door
            int newDoor = rng.Range(0,maxForDoor);
            //create children
            RectInt topArea = new RectInt(area.x, area.y + split * WALLWIDTH, area.width, area.height - split * WALLWIDTH);
            RectInt bottomArea = new RectInt(area.x, area.y, area.width, split * WALLWIDTH);

            this.left = new BSPNode(bottomArea);
            this.right = new BSPNode(topArea);

            //pass information to children
                   //new top and bot doors
            right.topDoor = this.topDoor;
            right.bottomDoor = newDoor;

            left.topDoor = newDoor;
            left.bottomDoor = this.bottomDoor;
                    //update right and left doors
            if(split <= this.leftDoor)right.leftDoor = this.leftDoor - split;
            else left.leftDoor  = this.leftDoor ;
            if(split <= this.rightDoor)right.rightDoor = this.rightDoor - split;
            else left.rightDoor = this.rightDoor ;


        }
        else
        {
            
            //choose wallPiece to split from
            int split = rng.Range(minWallNum, maxForSplit);
            //choose wallPiece to become a door
            int newDoor = rng.Range(0, maxForDoor);
            
            //create children
            RectInt leftArea = new RectInt(area.x, area.y, split * WALLWIDTH, area.height);
            RectInt rightArea = new RectInt(area.x + split * WALLWIDTH, area.y, area.width - split * WALLWIDTH, area.height);
            this.left = new BSPNode(leftArea);
            this.right = new BSPNode(rightArea);

            //pass information to children
                //new right and left doors 
            right.rightDoor = this.rightDoor;
            right.leftDoor = newDoor;

            left.rightDoor = newDoor;
            left.leftDoor = this.leftDoor;

                //update old top and bot doors
            if(split<=this.bottomDoor)right.bottomDoor = this.bottomDoor - split;
            else left.bottomDoor = this.bottomDoor ;
            if(split<=this.topDoor) right.topDoor = this.topDoor - split ;
            else left.topDoor = this.topDoor  ;
        }


        right.SplitWithDoors(minWallNum, rng, allowBigRooms);
        left.SplitWithDoors(minWallNum, rng, allowBigRooms);


    }





    public void Split(int minSize, bool allowBigRooms, SeededRandom rng)
    {
        if (!isLeaf) return; //only try to split those that don't have have children
        
        bool splitH = rng.Range(0.0f, 1.0f) > 0.5f; //split Horizontally ?
        /*
        if (splitH && area.height < minSize * 2) return;
        if (!splitH && area.width < minSize * 2) return;
*/
        

        int max = (splitH ? area.height : area.width) - minSize; //choose the maximum position that the split line can take
        if (allowBigRooms) { if (max <= minSize) return; }
        
        else {
            if (max <= minSize)
            {

                splitH = !splitH;
                max = (splitH ? area.height : area.width) - minSize; //try again in the other direction


                if (max <= minSize) return; //splitting not possible
            }
        }



        int split = rng.Range(minSize, max); //split line position (integer)
        
        
        /**/
        if (splitH) //choose left and right children (ken horizontal fou9 wlouta si nn isar w imin)
        {
            RectInt newArea = new RectInt(area.x, area.y, area.width, split); //the part above the split line
            this.left = new BSPNode(newArea);
            
            newArea = new RectInt(area.x,area.y+split,area.width,area.height-split); // the part under the split line
            this.right = new BSPNode(newArea);

        }
        else
        {
            RectInt newArea = new RectInt(area.x, area.y, split, area.height); //the part on the left
            this.left = new BSPNode(newArea);

            newArea = new RectInt(area.x + split, area.y, area.width-split, area.height); // the part on the right
            this.right = new BSPNode(newArea);


        }

        left.Split(minSize, allowBigRooms , rng);
        right.Split(minSize, allowBigRooms ,rng);


    }
    
    public void Split(int minSize, bool allowBigRooms)
    {
        if (!isLeaf) return; //only try to split those that don't have have children

        int seed = Random.Range(0, 500);
        SeededRandom rng = new SeededRandom(seed);

        bool splitH = rng.Range(0.0f, 1.0f) > 0.5f; //split Horizontally ?
       
       

        int max = (splitH ? area.height : area.width) - minSize; //choose the maximum position that the split line can take
        
        if (allowBigRooms) { if (max <= minSize) return; }

        else
        {
            if (max <= minSize)
            {

                splitH = !splitH;
                max = (splitH ? area.height : area.width) - minSize; //try again in the other direction


                if (max <= minSize) return; //splitting not possible
            }
        }


        int split = rng.Range(minSize, max); //split line position (integer)
        
        
        /**/
        if (splitH) //choose left and right children (ken horizontal fou9 wlouta si nn isar w imin)
        {
            RectInt newArea = new RectInt(area.x, area.y, area.width, split); //the part above the split line
            this.left = new BSPNode(newArea);
            
            newArea = new RectInt(area.x,area.y+split,area.width,area.height-split); // the part under the split line
            this.right = new BSPNode(newArea);

        }
        else
        {
            RectInt newArea = new RectInt(area.x, area.y, split, area.height); //the part on the left
            this.left = new BSPNode(newArea);

            newArea = new RectInt(area.x + split, area.y, area.width-split, area.height); // the part on the right
            this.right = new BSPNode(newArea);


        }

        left.Split(minSize, allowBigRooms, rng);
        right.Split(minSize, allowBigRooms, rng);


    }

    public void CreateRooms(MainGenerator generator) //without a seed
    {
        if (isLeaf)
        {
            float x = area.x + area.width/2f;
            float z = area.y + area.height/2f;

            //generate One room at each leaf
            //------------------------------------------------------generator.GenerateOneRoom(area.width-2, area.height-2,0,new Vector3(x,0,z));

        }
        else
        {
            left.CreateRooms(generator);
            right.CreateRooms(generator);
        }
       
    } 
    
    public void CreateRooms(MainGenerator generator, SeededRandom rng) //with a seed
    {
        if (isLeaf)
        {
            float x = area.x + area.width/2f;
            float z = area.y + area.height/2f;

            //generate One room at each leaf
            generator.GenerateOneRoom(area.width-2, area.height-2,topDoor,bottomDoor,rightDoor,leftDoor,new Vector3(x,0,z), rng);

        }
        else
        {
            left.CreateRooms(generator, rng);
            right.CreateRooms(generator, rng);
        }
       
    }


public int ChooseWithSkip(int min, int max, int skipA, int skipB, SeededRandom rng)
    {

        int validNums = max - min + 1 - (skipA > 1 ? 1 : 0) - (skipB > 1 ? 1 : 0);
        if (validNums <= 0) return -1;
        
        //favorise splitting between doors
        if (Mathf.Abs(skipA - skipB) != 1)
        {
            return rng.Range(Mathf.Min(skipA,skipB)+1, Mathf.Max(skipA,skipB)-1);
        }
        //otherwise split split before the least value or after it randomly
        return -1;
    }



}
