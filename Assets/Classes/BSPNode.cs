using UnityEngine;

public class BSPNode
{
    public RectInt area;

    public BSPNode left;
    public BSPNode right;

    public SeededRandom rng;
    public bool isLeaf => left == null && right == null;

    public BSPNode(RectInt area)
    {
        this.area = area;
        
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
            //-----------------------------------------------generator.GenerateOneRoom(area.width-2, area.height-2,0,new Vector3(x,0,z), rng);

        }
        else
        {
            left.CreateRooms(generator, rng);
            right.CreateRooms(generator, rng);
        }
       
    }






}
