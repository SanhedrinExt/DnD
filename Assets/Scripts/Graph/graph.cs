using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Graph {

    public const int c_y = 3;
    public const int c_x = 3;

    private Graph() 
    {
        for(int y = 0 ;y < c_y ;++y)
        {
            for(int x = 0;x < c_x;++x)
            {
                m_rooms[y, x] = new RoomNode(x ,y);
            }
        }
    }

    static private Graph s_singltonGraph;

    public static Graph GraphSingleton
    {
        get
        {
            if (s_singltonGraph == null)
            {
                s_singltonGraph = new Graph();
            }
            return s_singltonGraph;
        }
        private set { s_singltonGraph = value; }
    }

    public RoomNode[,] m_rooms = new RoomNode[c_y, c_x];

    public void ConactRoomScripToRoomNode(Vector2 v2 ,RoomScript roomS)
    {
        m_rooms[(int)v2.y,(int)v2.x].refRoom = roomS;
        roomS.roomGraphNode = m_rooms[(int)v2.y, (int)v2.x];
    }

    public void RemovRoom(Vector2 i_roomVec)
    {
        RoomNode roomToremove = m_rooms[(int)i_roomVec.y, (int)i_roomVec.x];
        roomToremove.DeactivatRoom();
        
        while(IsGrathConacted() == false) // בהחרך קיים שחן שחור ואחד לבן
        {
            ConactNaibers(roomToremove);
        }
    }

    private void ConactNaibers(RoomNode i_badRoom)
    {
       
        foreach(RoomNode room in i_badRoom.m_niebringRooms)
        {
            if(room.m_eColor == eColor.White)
            {
                ConactMe(room.myPosition, i_badRoom.myPosition);
                break;
            }
        }
       
    }
    private BfsNode findSortPath(RoomNode i_start , RoomNode i_end)
    {
        Queue<BfsNode> queue = new Queue<BfsNode>();
        List<Vector2> arcivs = new List<Vector2>();
        queue.Enqueue(new BfsNode(i_start.myPosition, null, 0));
        BfsNode solver = null;

        while (solver == null || queue.Count == 0)
        {
            BfsNode node = queue.Dequeue();
            if (m_rooms[(int)node.mySpot.y, (int)node.mySpot.x].m_niebringRooms == null) continue;
            foreach (RoomNode room in m_rooms[(int)node.mySpot.y, (int)node.mySpot.x].m_niebringRooms)
            {
                if (isRoomVisabol(room) != true) continue;
                BfsNode bfsNode = new BfsNode(room.myPosition, node, node.depth + 1);
                if (room == i_end)
                {
                    solver = bfsNode;
                    break;
                }
                if (arcivs.Contains(bfsNode.mySpot) == false)
                {
                    arcivs.Add(bfsNode.mySpot);
                    queue.Enqueue(bfsNode);
                }
            }
        }
        return solver;
    }
    private void ConactMe(Vector2 S, Vector2 i_badRoom)
    {
        Queue<BfsNode> queue = new Queue<BfsNode>();
        List<Vector2> arcivs = new List<Vector2>();
        arcivs.Add(S); arcivs.Add(i_badRoom);
        queue.Enqueue(new BfsNode(S, null, 0));
        BfsNode solver = null;

        while (solver == null)
        {
            BfsNode node = queue.Dequeue();

            List<BfsNode> naeibers = getGoodNaeibersForBfs(node, arcivs);
            foreach(BfsNode naeiber in naeibers)
            {
                if (m_rooms[(int)naeiber.mySpot.y, (int)naeiber.mySpot.x].m_niebringRooms != null && m_rooms[(int)naeiber.mySpot.y, (int)naeiber.mySpot.x].m_activRoom == true && m_rooms[(int)naeiber.mySpot.y, (int)naeiber.mySpot.x].m_eColor == eColor.Black)
                {
                    solver = naeiber;
                    break;
                }
                else
                {
                    queue.Enqueue(naeiber);
                }
            }
        }
        ReconctingGrath(solver);
    }

private void ReconctingGrath(BfsNode i_goodStart)
{
    BfsNode son = i_goodStart ;
    BfsNode dad = son.perent;

    while(m_rooms[(int)dad.mySpot.y,(int)dad.mySpot.x].m_activRoom == false)
    {
        m_rooms[(int)dad.mySpot.y, (int)dad.mySpot.x].ActivatRoom(new List<RoomNode> { m_rooms[(int)son.mySpot.y, (int)son.mySpot.x] });
        son = dad;
        dad = dad.perent;
    }
    m_rooms[(int)dad.mySpot.y, (int)dad.mySpot.x].m_niebringRooms.Add(m_rooms[(int)son.mySpot.y, (int)son.mySpot.x]);
    m_rooms[(int)son.mySpot.y, (int)son.mySpot.x].m_niebringRooms.Add(m_rooms[(int)dad.mySpot.y, (int)dad.mySpot.x]);
}

private List<BfsNode> getGoodNaeibersForBfs(BfsNode i_node,List<Vector2> i_arcivs)
{
    List<BfsNode> nodss = new List<BfsNode>();
    int x = (int)i_node.mySpot.x; 
    int y = (int)i_node.mySpot.y;
    

    if( x + 1 < c_x && i_arcivs.Contains(m_rooms[y,x + 1].myPosition ) != true )
    {
        i_arcivs.Add(m_rooms[y, x + 1].myPosition);
        nodss.Add(new BfsNode(m_rooms[y, x + 1].myPosition, i_node, i_node.depth + 1));
    }

    if (x  != 0 && i_arcivs.Contains(m_rooms[y, x - 1].myPosition) != true)
    {
        i_arcivs.Add(m_rooms[y, x - 1].myPosition);
        nodss.Add(new BfsNode(m_rooms[y, x - 1].myPosition, i_node, i_node.depth + 1));
    }

    if (y + 1 < c_y && i_arcivs.Contains(m_rooms[y + 1, x ].myPosition) != true)
    {
        i_arcivs.Add(m_rooms[y + 1,x].myPosition);
        nodss.Add(new BfsNode(m_rooms[y + 1, x].myPosition, i_node, i_node.depth + 1));
    }

    if (y != 0 && i_arcivs.Contains(m_rooms[y - 1, x].myPosition) != true)
    {
        i_arcivs.Add(m_rooms[y - 1 , x].myPosition);
        nodss.Add(new BfsNode(m_rooms[y - 1, x].myPosition, i_node, i_node.depth + 1));
    }


    return nodss;
}



    private void ResetColor()
    {
        foreach(RoomNode room in m_rooms)
        {
            room.m_eColor = eColor.White;
        }
    }

    private void Visit(RoomNode i_room)
    {
        i_room.m_eColor = eColor.Grey;
        if (i_room.m_niebringRooms != null)
        {
            foreach (RoomNode room in i_room.m_niebringRooms)
            {
                if (room.m_eColor == eColor.White)
                {
                    Visit(room);
                }
            }
        }
        i_room.m_eColor = eColor.Black;
    }
    
    private void Dfs()
    {
        ResetColor();
        foreach(RoomNode room in m_rooms)
        {
            if(room.m_activRoom == true)
            {
                Visit(room);
            }
        }
    }

    private bool IsGrathConacted()
    {
        bool res = true;

        Dfs();

        foreach (RoomNode room in m_rooms)
        {
            if (room.m_activRoom == true && room.m_eColor == eColor.White)
            {
                res = false;
            }
        }   
        return res;
    }

    public Stack<Vector3> GetVectorPath(Vector3 i_strt, Vector3 i_end)
    {
        if (IsGrathConacted() == false)
            Debug.LogError("grath is not conacted");
        Stack<Vector3> moveQ = null;
        RoomNode startR = convertV3toRoomNode(i_strt);
        RoomNode endR = convertV3toRoomNode(i_end);

        
        if (startR != null && endR != null && isRoomReachbol(endR) != true)
        {
            if (startR != endR)
            {
                BfsNode bfsNode = findSortPath(startR, endR);
                moveQ = getV3PathFromBfsNode(i_end, bfsNode);
            }
            else
            {
                moveQ = new Stack<Vector3>();
                moveQ.Push(i_end);
            }
        }
        else
        {
            moveQ = new Stack<Vector3>();
        }

        return moveQ;
    }

    private bool isRoomVisabol(RoomNode i_room)
    {
        return i_room.refRoom.GetComponent<Renderer>().enabled;
    }

    private bool isRoomReachbol(RoomNode i_room)
    {
        bool res = false;

        foreach( RoomNode room in i_room.m_niebringRooms)
        {
            if(isRoomVisabol(room) == true )
            {
                res = true;
                break;
            }
        }
        return res;
    }

    private Stack<Vector3> getV3PathFromBfsNode(Vector3 i_Target, BfsNode i_bfsNode)
    {
        Stack<Vector3> stack = new Stack<Vector3>();
        if (i_bfsNode != null)
        {
            BfsNode bfsNode = i_bfsNode;

            stack.Push(i_Target);

            while (bfsNode != null)
            {
                stack.Push(m_rooms[(int)bfsNode.mySpot.y, (int)bfsNode.mySpot.x].refRoom.transform.position);
                bfsNode = bfsNode.perent;
            }
        }

        return stack;
    }

   public void ConactingRooms(Vector2 v1 , Vector2 v2)
    {
        m_rooms[(int)v1.y, (int)v1.x].m_niebringRooms.Add(m_rooms[(int)v2.y, (int)v2.x]);
        m_rooms[(int)v2.y, (int)v2.x].m_niebringRooms.Add(m_rooms[(int)v1.y, (int)v1.x]);
    }

    private RoomNode convertV3toRoomNode(Vector3 v3)
    {
        RoomNode res = null;
        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(v3.x, v3.y), Vector2.zero, 1);
        foreach (RaycastHit2D hit in hits)
        {
            RoomScript roomS = hit.transform.GetComponent<RoomScript>();
            if (roomS != null)
            {
                res = roomS.roomGraphNode;
                break;
            }
        }
        return res;
    }
	
}
