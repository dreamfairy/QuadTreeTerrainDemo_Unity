﻿//This code came from book 《Focus On 3D Terrain Programming》 ,thanks Trent Polack a lot
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Utility;
using Assets.Scripts.Common;

namespace Assets.Scripts.QuadTree
{

    #region 结点定义 

    class CQuadTreeNode
    {
        public CQuadTreeNode mTopLeftNode;
        public CQuadTreeNode mTopRightNode;
        public CQuadTreeNode mBottomRightNode;
        public CQuadTreeNode mBottomLetfNode;

        public bool mbSubdivide;
        public int mIndexX;
        public int mIndexZ;
        public enNodeType mNodeType;

        public CQuadTreeNode(int x, int z)
        {
            mIndexX = x;
            mIndexZ = z;
            mbSubdivide = true;
        }
    }

    enum enNodeTriFanType
    {
        Complete_Fan = 0, //全部划三角形
        BottomLeft_TopLeft_TopRight = 1,
        TopLeft_TopRight_BottomRight = 2,
        TopLeft_TopRight = 3,
        TopRight_BottomRight_BottomLeft = 4,
        BottomLeft_TopRight = 5,
        TopRight_BottomRight = 6,
        TopRight = 7,
        BottomRight_BottomLeft_TopLeft = 8,
        BottomLeft_TopLeft = 9,
        BottomRight_TopLeft = 10,
        TopLeft = 11,
        BottomLeft_BottomRight = 12,
        BottomLeft = 13,
        BottomRight = 14, //右下划三角形
        No_Fan = 15  //全部子结点都要进行下一部划分 
    }


    //哪个位置的三角形
    enum enFanPosition
    {
        One = 1,
        Two = 2,
        Four = 4,
        Eight = 8,
    }


    //节点的位置
    enum enNodeType
    {
        Root = 0,
        BottomRight = 1,
        BottomLeft = 2,
        TopLeft = 4,
        TopRight = 8,
    }



    struct stFanGenerator
    {
        private bool mbDrawLeftMid;
        private bool mbDrawTopMid;
        private bool mbDrawRightMid;
        private bool mbDrawBottomMid;

        private stVertexAtrribute mCenterVertex;
        private stVertexAtrribute mBottomLeftVertex;
        private stVertexAtrribute mLeftMidVertex;
        private stVertexAtrribute mTopLeftVertex;
        private stVertexAtrribute mTopMidVertex;
        private stVertexAtrribute mTopRightVertex;
        private stVertexAtrribute mRightMidVertex;
        private stVertexAtrribute mBottomRightVertex;
        private stVertexAtrribute mBottomMidVertex;


        public stFanGenerator(
            bool drawLeftMid,
            bool drawTopMid,
            bool drawRightMid,
            bool drawBottomMid,
            stVertexAtrribute centerVertex,
            stVertexAtrribute bottomLeftVertex,
            stVertexAtrribute leftMidVertex,
            stVertexAtrribute topLeftVertex,
            stVertexAtrribute topMidVertex,
            stVertexAtrribute topRightVertex,
            stVertexAtrribute rightMidVertex,
            stVertexAtrribute bottomRightVertex,
            stVertexAtrribute bottomMidVertex
            )
        {
            mbDrawLeftMid = drawLeftMid;
            mbDrawTopMid = drawTopMid;
            mbDrawRightMid = drawRightMid;
            mbDrawBottomMid = drawBottomMid;

            mCenterVertex = centerVertex;
            mBottomLeftVertex = bottomLeftVertex;
            mLeftMidVertex = leftMidVertex;
            mTopLeftVertex = topLeftVertex;
            mTopMidVertex = topMidVertex;
            mTopRightVertex = topRightVertex;
            mRightMidVertex = rightMidVertex;
            mBottomRightVertex = bottomRightVertex;
            mBottomMidVertex = bottomMidVertex;
        }


        public void DrawFan(ref stTerrainMeshData meshData, enFanPosition fanPos)
        {
            switch (fanPos)
            {
                #region Draw 1
                case enFanPosition.One:
                    {
                        ///////////////////////Draw 1 ////////////////////////
                        //Bottom Right 2 Triangle
                        if (mbDrawRightMid && mbDrawBottomMid)
                        {
                            ///1 center  right mid  bottom right 
                            meshData.RenderTriangle(mCenterVertex, mRightMidVertex, mBottomRightVertex);
                            ///2 center  bottom right bottom mid 
                            meshData.RenderTriangle(mCenterVertex, mBottomRightVertex, mBottomMidVertex);
                        }
                        else if (mbDrawRightMid)
                        {
                            ///1 center  right mid  bottom right 
                            meshData.RenderTriangle(mCenterVertex, mRightMidVertex, mBottomRightVertex);
                            ///2 center  bottom right bottom left 
                            meshData.RenderTriangle(mCenterVertex, mBottomRightVertex, mBottomLeftVertex);
                        }
                        else if (mbDrawBottomMid)
                        {
                            ///1 center  top right  bottom right 
                            meshData.RenderTriangle(mCenterVertex, mTopRightVertex, mBottomRightVertex);
                            ///2 center  bottom right bottom mid 
                            meshData.RenderTriangle(mCenterVertex, mBottomRightVertex, mBottomMidVertex);
                        }
                        else
                        {
                            meshData.RenderTriangle(mCenterVertex, mTopRightVertex, mBottomRightVertex);
                            meshData.RenderTriangle(mCenterVertex, mBottomRightVertex, mBottomLeftVertex);
                        }

                        /////////////////////Draw 1 ////////////////////////////////
                        break;
                    }

                #endregion

                #region Draw 2
                case enFanPosition.Two:
                    {

                        ///////////////// Draw 2 ////////////////////////
                        //Bottom Left 2 Triangle
                        if (mbDrawLeftMid && mbDrawBottomMid)
                        {
                            ///1 center bottom mid bottom left 
                            meshData.RenderTriangle(mCenterVertex, mBottomMidVertex, mBottomLeftVertex);
                            ///2 center  bottom left  left mid 
                            meshData.RenderTriangle(mCenterVertex, mBottomLeftVertex, mLeftMidVertex);
                        }
                        else if (mbDrawLeftMid)
                        {
                            ///1 center bottom mid bottom left 
                            meshData.RenderTriangle(mCenterVertex, mBottomRightVertex, mBottomLeftVertex);
                            ///2 center  bottom left  left mid 
                            meshData.RenderTriangle(mCenterVertex, mBottomLeftVertex, mLeftMidVertex);
                        }
                        else if (mbDrawBottomMid)
                        {
                            ///1 center bottom mid bottom left 
                            meshData.RenderTriangle(mCenterVertex, mBottomMidVertex, mBottomLeftVertex);

                            ///2 center  bottom left  top left 
                            meshData.RenderTriangle(mCenterVertex, mBottomLeftVertex, mTopLeftVertex);
                        }
                        else
                        {
                            ///1 center bottom mid bottom left 
                            meshData.RenderTriangle(mCenterVertex, mBottomRightVertex, mBottomLeftVertex);
                            ///2 center  bottom left  top left 
                            meshData.RenderTriangle(mCenterVertex, mBottomLeftVertex, mTopLeftVertex);
                        }
                        /////////////////////Draw 2 //////////////////////////////
                        break;
                    }

                #endregion

                #region Draw 4
                case enFanPosition.Four:
                    {
                        //////////////Draw 4 ///////////////////////
                        //Top Left 2 Triangle
                        if (mbDrawLeftMid && mbDrawTopMid)
                        {
                            ///1 center left mid top left
                            meshData.RenderTriangle(mCenterVertex, mLeftMidVertex, mTopLeftVertex);
                            ///2 centert top left top mid
                            meshData.RenderTriangle(mCenterVertex, mTopLeftVertex, mTopMidVertex);
                        }
                        else if (mbDrawLeftMid)
                        {
                            ///1 center left mid top left
                            meshData.RenderTriangle(mCenterVertex, mLeftMidVertex, mTopLeftVertex);
                            ///2 centert top left top right
                            meshData.RenderTriangle(mCenterVertex, mTopLeftVertex, mTopRightVertex);
                        }
                        else if (mbDrawTopMid)
                        {
                            ///1 center left mid top left
                            meshData.RenderTriangle(mCenterVertex, mBottomLeftVertex, mTopLeftVertex);
                            ///2 centert top left top mid
                            meshData.RenderTriangle(mCenterVertex, mTopLeftVertex, mTopMidVertex);
                        }
                        else
                        {
                            ///1 center left mid top left
                            meshData.RenderTriangle(mCenterVertex, mBottomLeftVertex, mTopLeftVertex);
                            ///2 centert top left top right
                            meshData.RenderTriangle(mCenterVertex, mTopLeftVertex, mTopRightVertex);
                        }
                        /////////////////Draw 4 ///////////////////


                        break;
                    }
                #endregion

                #region Draw 8
                case enFanPosition.Eight:
                    {
                        //Top Right  Triangle 
                        if (mbDrawTopMid && mbDrawRightMid)
                        {
                            ///1 center top mid top right
                            meshData.RenderTriangle(mCenterVertex, mTopMidVertex, mTopRightVertex);
                            ///2 center top right right mid
                            meshData.RenderTriangle(mCenterVertex, mTopRightVertex, mRightMidVertex);
                        }
                        else if (mbDrawTopMid)
                        {
                            ///1 center top mid top right
                            meshData.RenderTriangle(mCenterVertex, mTopMidVertex, mTopRightVertex);
                            // 2 center top right bottom right
                            meshData.RenderTriangle(mCenterVertex, mTopRightVertex, mBottomRightVertex);
                        }
                        else if (mbDrawRightMid)
                        {
                            ///1 center top mid top right
                            meshData.RenderTriangle(mCenterVertex, mTopLeftVertex, mTopRightVertex);
                            ///2 center top right right mid
                            meshData.RenderTriangle(mCenterVertex, mTopRightVertex, mRightMidVertex);
                        }
                        else
                        {
                            ///1 center top mid top right
                            meshData.RenderTriangle(mCenterVertex, mTopLeftVertex, mTopRightVertex);
                            // 2 center top right bottom right
                            meshData.RenderTriangle(mCenterVertex, mTopRightVertex, mBottomRightVertex);
                        }
                        break;
                    }

                    #endregion

            }

        }

    }





    #endregion


    class CQuadTreeTerrain
    {

        #region   核心逻辑
    
        public List<CQuadTreeNode> mNodeList = new List<CQuadTreeNode>();  

        private int GetIndex( int x, int z )
        {
            return z * mHeightData.mSize + x; 
        }

        private CQuadTreeNode GetNode( int x, int z )
        {
            //不合法的输入直接排队
            if( x < 0 || x >= mHeightData.mSize || z < 0 || z >= mHeightData.mSize )
            {
                return null; 
            }

            int idx = GetIndex(x, z);
            return  (idx >=0 && idx < mNodeList.Count) ? mNodeList[idx] : null;     
        }


        public void GenerateNodes( int size )
        {
            mNodeList.Clear();
            for(int z = 0; z < size; ++z )
            {
                for(int x  = 0; x < size; ++x )
                {
                    CQuadTreeNode node = new CQuadTreeNode(x, z);
                    mNodeList.Add(node);  
                }
            }

            Debug.Log(string.Format("[GenerateNodes Done] {0} ",mNodeList.Count));  
        }


        public void ResetNodeSubdivide()
        {
            for( int i = 0; i < mNodeList.Count; ++i )
            {
                mNodeList[i].mbSubdivide = false; 
            }
        }


        //如果当前结点需要拆分，但是如果 既是父结点，也是当前结点的邻结点不拆分的话，那当前结点也不能够，不然的话，会出现裂缝
        private bool CanNodeDivide( CQuadTreeNode childNode , CQuadTreeNode parentANeighborNode , CQuadTreeNode parentBNeighborNode )
        {
            bool bRet = false;
            if(  childNode != null  )
            {
                bRet = childNode.mbSubdivide;  

                if( parentANeighborNode != null )
                {
                    bRet &= parentANeighborNode.mbSubdivide;  //还要看看紧贴的结点是否也要拆
                }
                if( parentBNeighborNode != null )
                {
                    bRet &= parentBNeighborNode.mbSubdivide; 
                }
            }
            return bRet; 
        }


        public void PropagateRoughness(float minResolution, float desResolution)
        {
            //先重置一波
            mRoughnessData.Reset(0);

            int iCurNodeLength = 3;  //最小结点
            while (iCurNodeLength <= mHeightData.mSize)
            {
                int iHalfLengthOffset = (iCurNodeLength - 1) >> 1;   //半个结点长度，用来求出相邻结点
                int iChildHalfLengthOffset = (iCurNodeLength - 1) >> 2; //半个子结点的长度，用来求出相邻子结点

                for (int z = iHalfLengthOffset; z < mHeightData.mSize; z += (iCurNodeLength - 1))
                {
                    for (int x = iHalfLengthOffset; x < mHeightData.mSize; x += (iCurNodeLength - 1))
                    {
                        int xLeftCur = x - iHalfLengthOffset;
                        int zBottomCur = z - iHalfLengthOffset;
                        int xRightCur = x + iHalfLengthOffset;
                        int zTopCur = z + iHalfLengthOffset;

                        int xLeftChild = x - iChildHalfLengthOffset;
                        int xRightChild = x + iChildHalfLengthOffset;
                        int zTopChild = z + iChildHalfLengthOffset;
                        int zBottomChild = z - iChildHalfLengthOffset;

                        int topLeftHeight = mHeightData.GetRawHeightValue(xLeftCur, zTopCur);
                        int topRightHeight = mHeightData.GetRawHeightValue(xRightCur, zTopCur);
                        int bottomLeftHeight = mHeightData.GetRawHeightValue(xLeftCur, zBottomCur);
                        int bottomRightHeight = mHeightData.GetRawHeightValue(xRightCur, zBottomCur);
                        int topMidHeight = mHeightData.GetRawHeightValue(x, zTopCur);
                        int rightMidHeight = mHeightData.GetRawHeightValue(xRightCur, z);
                        int bottomMidHeight = mHeightData.GetRawHeightValue(x, zBottomCur);
                        int leftMidHeight = mHeightData.GetRawHeightValue(xLeftCur, z);
                        int centerHeight = mHeightData.GetRawHeightValue(x, z);

                        float topMidHeightDiff = Mathf.Abs(((float)(topLeftHeight + topRightHeight) / 2.0f) - topMidHeight);
                        float rightMidHeightDiff = Mathf.Abs(((float)(topRightHeight + bottomRightHeight) /2.0f) - rightMidHeight);
                        float bottomMidHeightDiff = Mathf.Abs(((float)(bottomLeftHeight + bottomRightHeight) / 2.0f) - bottomMidHeight);
                        float leftMidHeightDiff = Mathf.Abs(((float)(topLeftHeight + bottomLeftHeight) / 2.0f) - leftMidHeight);
                        float bottomLeft2TopRightHeightDiff = Mathf.Abs(((float)(bottomLeftHeight + topRightHeight)  / 2.0f) - centerHeight);
                        float bottomRight2TopLeftHeightDiff = Mathf.Abs(((float)(bottomRightHeight + topLeftHeight) / 2.0f) - centerHeight);

                        //取出最大的差值 
                        float maxHeightDiff = Mathf.Max(leftMidHeightDiff, topMidHeightDiff, rightMidHeightDiff, bottomMidHeightDiff, bottomLeft2TopRightHeightDiff, bottomRight2TopLeftHeightDiff);

                        float localD2 = maxHeightDiff / iCurNodeLength ;  //根据论文的公式 d2 =  d2Max / d 
                        if( iCurNodeLength != 3 ) //如果不是叶子结点
                        {
                            //论文的公式： K = C / (2C - 1)，上边界
                            float fKupperBound = minResolution / (2.0f * (minResolution - 1));

                            //这里也是根据论文的内容 
                            //The d2-value of each block is the maximum
                            //of the local value and K times the previously
                            //calculated values of adjacent blocks at the next lower
                            //level.
                         
                            //上一个Lod计算出来的D2值
                            float fLeftChildMidD2 = mRoughnessData.GetRoughnessValue(xLeftChild, z) * fKupperBound;
                            float fRightChildMidD2 = mRoughnessData.GetRoughnessValue(xRightChild, z) * fKupperBound;
                            float fTopChildMidD2 = mRoughnessData.GetRoughnessValue(x, zTopChild) * fKupperBound;
                            float fBottomChildMidD2 = mRoughnessData.GetRoughnessValue(x, zBottomChild) * fKupperBound;

                            //取出最大值
                            localD2 = (int)Mathf.Max(
                                localD2,
                                fLeftChildMidD2,
                                fRightChildMidD2,
                                fTopChildMidD2,
                                fBottomChildMidD2
                                );
                        }  // else 


                        float tCenterD2 = mRoughnessData.GetRoughnessValue(x, z);
                        localD2 = Mathf.Max(tCenterD2, localD2); 
                        mRoughnessData.SetRoughnessValue(localD2, x, z);
                   
                        //推荐的四个的D2值
                        float bottomLeftD2 = Mathf.Max(mRoughnessData.GetRoughnessValue(xLeftCur, zBottomCur), localD2);
                        float topLeftD2 = Mathf.Max(mRoughnessData.GetRoughnessValue(xLeftCur, zTopCur), localD2);
                        float topRightD2 = Mathf.Max(mRoughnessData.GetRoughnessValue(xRightCur, zTopCur), localD2);
                        float bottomRightD2 = Mathf.Max(mRoughnessData.GetRoughnessValue(xRightCur, zBottomCur), localD2);


                        //传递到四个顶点，其实不太明白这样的做的原因
                        mRoughnessData.SetRoughnessValue(bottomLeftD2, xLeftCur, zBottomCur);
                        mRoughnessData.SetRoughnessValue(topLeftD2, xLeftCur, zTopCur);
                        mRoughnessData.SetRoughnessValue(topRightD2, xRightCur, zTopCur);
                        mRoughnessData.SetRoughnessValue(bottomRightD2, xRightCur, zBottomCur);

                    }  // for 
                }  // for 

                iCurNodeLength = (iCurNodeLength << 1) - 1;

            }  //while 
        }  //function


        public CQuadTreeNode RefineNode(
             float x,
             float z,
             int curNodeLength,   //暂时定为节点的个数
             Camera viewCamera,
             Vector3 vectorScale,
             float desiredResolution,
             float minResolution,
             bool useRoughnessEvalute 
             )
        {
            //这个其实只要运行一次就够了，但是为了可以演示的时候运行调整，放在每次更新的时候了
            Profiler.BeginSample("QuadTree.PropagateRoughness");
            if ( useRoughnessEvalute )
            {
                PropagateRoughness(minResolution, desiredResolution); 
            }
            Profiler.EndSample(); 

            return RefineNodeImpl(x, z, curNodeLength, enNodeType.Root, null, null, null, null, viewCamera, vectorScale, desiredResolution, minResolution,useRoughnessEvalute);
        }

        public CQuadTreeNode RefineNodeImpl( 
            float x ,
            float z ,
            int curNodeLength ,   //暂时定为节点的个数
            enNodeType nodeType ,
            CQuadTreeNode parentTopNeighborNode ,
            CQuadTreeNode parentRightNeighborNode ,
            CQuadTreeNode parentBottomNeighborNode ,
            CQuadTreeNode parentLeftNeighborNode, 
            Camera viewCamera ,
            Vector3 vectorScale, 
            float desiredResolution,
            float minResolution,
            bool useRoughnessEvalute 
            )
        {
            if( null == viewCamera )
            {
                Debug.LogError("[RefineNode]View Camera is Null!");
                return null; 
            }

            int tX = (int)x;
            int tZ = (int)z;

            CQuadTreeNode qtNode = GetNode(tX, tZ); 
            if( null == qtNode )
            {
                Debug.LogError( string.Format("[RefineNode]No Such Node at :{0}|{1}",tX,tZ));
                return null ; 
            }

            qtNode.mNodeType = nodeType; 

            //评价公式
            ushort nodeHeight = GetTrueHeightAtPoint(qtNode.mIndexX, qtNode.mIndexZ);
            float fViewDistance = Mathf.Sqrt(
                Mathf.Pow(viewCamera.transform.position.x - qtNode.mIndexX * vectorScale.x, 2) +
                Mathf.Pow(viewCamera.transform.position.y - nodeHeight * vectorScale.y, 2) +
                Mathf.Pow(viewCamera.transform.position.z - qtNode.mIndexZ * vectorScale.z, 2)
                  );

           
            //1、没有高度的话，单纯就判断水平上的距离来判断是否拆分
            //2、有高度的话，
            //float fDenominator = vectorScale.y == 0 ?
            //    Mathf.Max(curNodeLength * vectorScale.x * minResolution, 1.0f) :
            //    (curNodeLength * minResolution * Mathf.Max(desiredResolution * nodeHeight / 3, 1.0f));
            //float f = fViewDistance / fDenominator;
            float f = 0; 
            if(useRoughnessEvalute)
            {
                float d = (curNodeLength - 1) * vectorScale.x;
                float d2 = mRoughnessData.GetRoughnessValue(tX, tZ);
                float C = minResolution;
                float c = desiredResolution; 
                float fDenominator = vectorScale.y == 0 ?
                   (Mathf.Max(d * Mathf.Max(c, C), 1.0f)) :
                   (d * C * Mathf.Max( c * d2  , 1.0f) );
                f = fViewDistance / fDenominator; 
            }
            else
            {
                // l / dc < 1
                float d = ( curNodeLength - 1 ) * vectorScale.x;
                float C = Mathf.Max(minResolution, desiredResolution);

                float fDenominator = Mathf.Max(d * C, 1.0f) ;
                f = fViewDistance / fDenominator;
            }


            //这个其实是补丁，原本的算法没有的，这里作用是如果预处理之后，还是发生了crack的情况，就强行拆面
            //一个节点是否能够划分
            //1、满足评价
            //2、和相邻的结点不相差两个层级，也就当前结点和父结点的兄弟结点不能相差两个层级
            qtNode.mbSubdivide = f < 1.0f ? true : false;
            switch (qtNode.mNodeType)
            {
                case enNodeType.TopLeft:
                    {
                        qtNode.mbSubdivide &= CanNodeDivide(qtNode, parentTopNeighborNode, parentLeftNeighborNode);
                        break;
                    }
                case enNodeType.TopRight:
                    {
                        qtNode.mbSubdivide &= CanNodeDivide(qtNode, parentTopNeighborNode, parentRightNeighborNode);
                        break;
                    }
                case enNodeType.BottomRight:
                    {
                        qtNode.mbSubdivide &= CanNodeDivide(qtNode, parentRightNeighborNode, parentBottomNeighborNode);
                        break;
                    }
                case enNodeType.BottomLeft:
                    {
                        qtNode.mbSubdivide &= CanNodeDivide(qtNode, parentLeftNeighborNode, parentBottomNeighborNode);
                        break;
                    }
            }

            //决定是否画顶点的
            int iNeighborOffset = curNodeLength - 1;
            int iX = (int)x;
            int iZ = (int)z;

            int iZBottom = iZ - iNeighborOffset;
            int iZTop = iZ + iNeighborOffset;
            int iXLeft = iX - iNeighborOffset;
            int iXRight = iX + iNeighborOffset;

            CQuadTreeNode bottomNeighborNode = GetNode(iX, iZBottom);
            CQuadTreeNode rightNeighborNode = GetNode(iXRight, iZ);
            CQuadTreeNode topNeighborNode = GetNode(iX, iZTop);
            CQuadTreeNode leftNeighborNode = GetNode(iXLeft, iZ);


            if ( qtNode.mbSubdivide )
            {
                if( !(curNodeLength <= 3) )
                {
                    float fChildeNodeOffset = (float)((curNodeLength - 1) >> 2);
                    int tChildNodeLength = (curNodeLength + 1) >> 1;

                    //bottom-right
                    qtNode.mBottomRightNode = RefineNodeImpl(x + fChildeNodeOffset, z - fChildeNodeOffset, tChildNodeLength, enNodeType.BottomRight, topNeighborNode, rightNeighborNode, bottomNeighborNode, leftNeighborNode, viewCamera, vectorScale, desiredResolution, minResolution , useRoughnessEvalute);
                    //bottom-left
                    qtNode.mBottomLetfNode = RefineNodeImpl(x - fChildeNodeOffset, z - fChildeNodeOffset, tChildNodeLength, enNodeType.BottomLeft, topNeighborNode, rightNeighborNode, bottomNeighborNode, leftNeighborNode, viewCamera, vectorScale, desiredResolution, minResolution, useRoughnessEvalute);
                    //top-left 
                    qtNode.mTopLeftNode = RefineNodeImpl(x - fChildeNodeOffset, z + fChildeNodeOffset, tChildNodeLength, enNodeType.TopLeft, topNeighborNode, rightNeighborNode, bottomNeighborNode, leftNeighborNode, viewCamera, vectorScale, desiredResolution, minResolution, useRoughnessEvalute);
                    //top-right
                    qtNode.mTopRightNode = RefineNodeImpl(x + fChildeNodeOffset, z + fChildeNodeOffset, tChildNodeLength, enNodeType.TopRight, topNeighborNode, rightNeighborNode, bottomNeighborNode, leftNeighborNode, viewCamera, vectorScale, desiredResolution, minResolution, useRoughnessEvalute);
                }        
            }

            return qtNode; 
        }


        #endregion



        #region  将模型渲染上去


        public void DrawGizoms(ref stTerrainMeshData meshData , Vector3 vertexScale, float gizmosScale, Color gizmosColor)
        {
            Gizmos.color = gizmosColor;

            Mesh mesh = meshData.mMesh;
            if (null == mesh)
            {
                Debug.LogError("Terrain without Mesh");
                return;
            }

            meshData.Reset();

            for (int z = 0; z < mHeightData.mSize; ++z)
            {
                for (int x = 0; x < mHeightData.mSize; ++x)
                {
                    float y = mHeightData.GetRawHeightValue(x, z);
                    Gizmos.DrawSphere(new Vector3(x * vertexScale.x, y * vertexScale.y, z * vertexScale.z),gizmosScale);
                }
            }
        }


        public void CLOD_Render( ref stTerrainMeshData meshData , Vector3 vertexScale )
        {
            meshData.Reset(); 
            float fCenter = (mHeightData.mSize - 1) >> 1;

            RenderNode(fCenter, fCenter, mHeightData.mSize,ref meshData ,vertexScale);

            meshData.Present(); 
        }


        private Vector3  GetScaleVector3(float x,float z, Vector3 vectorScale )
        {
            return new Vector3(
                x * vectorScale.x,
                mHeightData.GetRawHeightValue((int)x,(int)z) * vectorScale.y,
                z * vectorScale.z 
                ); 
        }


        private stVertexAtrribute GenerateVertex(
            int vertexX,
            int vertexZ,
            float fX,
            float fZ,
            float uvX , float uvZ , Vector3 vectorScale )
        {
            return new stVertexAtrribute(
                 GetIndex(vertexX, vertexZ),
                 GetScaleVector3(fX, fZ, vectorScale),
                 new Vector2(uvX, uvZ)
                );
        }


        private void RenderNode( float fX ,float fZ ,int curNodeLength ,ref stTerrainMeshData meshData, Vector3 vectorScale  )
        {
            int tHeightMapSize = mHeightData.mSize; 
            int iX = (int)fX;
            int iZ = (int)fZ;

            CQuadTreeNode curNode = GetNode(iX, iZ); 
            if( null == curNode )
            {
                Debug.LogError(string.Format("[RenderNode]No Node at :{0}|{1}", iX, iZ));
                return; 
            }

            //当前节点边长的一半
            int iHalfNodeLength = (curNodeLength - 1) >> 1;
            float fHalfNodeLength = (curNodeLength - 1) / 2.0f;

            //中点左位置
            int iLeftX = iX - iHalfNodeLength;
            float fLeftX = fX - fHalfNodeLength;

            //中点右位置
            int iRightX = iX + iHalfNodeLength;
            float fRightX = fX + iHalfNodeLength;

            //顶点中点位置
            int iTopZ = iZ + iHalfNodeLength;
            float fTopZ = fZ + fHalfNodeLength;

            //底部中点位置
            int iBottomZ = iZ - iHalfNodeLength;
            float fBottomZ = fZ - fHalfNodeLength; 


            //边长减1 ？相邻节点的距离
            int iNeighborOffset = curNodeLength - 1;

            float fTexLeft = Mathf.Abs(fX - fHalfNodeLength) / tHeightMapSize;
            float fTexBottom = Mathf.Abs(fZ - fHalfNodeLength) / tHeightMapSize;
            float fTexRight = Mathf.Abs(fX+ fHalfNodeLength) / tHeightMapSize;
            float fTexTop = Mathf.Abs(fZ + fHalfNodeLength) / tHeightMapSize;

            float fTexMidX = (fTexLeft + fTexRight) / 2.0f;
            float fTexMidZ = (fTexBottom + fTexTop) / 2.0f;


            //决定是否画顶点的
            int iZBottom = iZ - iNeighborOffset;
            int iZTop = iZ + iNeighborOffset;
            int iXLeft = iX - iNeighborOffset;
            int iXRight = iX + iNeighborOffset; 

            CQuadTreeNode bottomNeighborNode = GetNode(iX,iZBottom );
            CQuadTreeNode rightNeighborNode = GetNode(iXRight, iZ);
            CQuadTreeNode topNeighborNode = GetNode(iX, iZTop);
            CQuadTreeNode leftNeighborNode = GetNode(iXLeft, iZ);

            //举一个例子，如果下边的邻结点没有，或者下面的同级的邻结点是需要划分的，即当前结点也需要划分
            bool bDrawBottomMidVertex = (iZBottom < 0) || (bottomNeighborNode != null && bottomNeighborNode.mbSubdivide);
            bool bDrawRightMidVertex = (iXRight >= tHeightMapSize) || (rightNeighborNode != null && rightNeighborNode.mbSubdivide);
            bool bDrawTopMidVertex = (iZTop >= tHeightMapSize) || (topNeighborNode != null && topNeighborNode.mbSubdivide);
            bool bDrawLeftMidVertex = (iXLeft< 0) || (leftNeighborNode != null && leftNeighborNode.mbSubdivide);


            //Center Vertex
            stVertexAtrribute tCenterVertex = GenerateVertex(iX, iZ, fX, fZ, fTexMidX, fTexMidZ, vectorScale);
            //Bottom Left Vertex 
            stVertexAtrribute tBottomLeftVertex = GenerateVertex(iLeftX, iBottomZ, fLeftX, fBottomZ, fTexLeft, fTexBottom, vectorScale);   
            //Left Mid Vertext
            stVertexAtrribute tLeftMidVertex = GenerateVertex(iLeftX, iZ, fLeftX, fZ, fTexLeft, fTexMidZ, vectorScale);
            //Top Left Vertex 
            stVertexAtrribute tTopLeftVertex = GenerateVertex(iLeftX, iTopZ, fLeftX, fTopZ, fTexLeft, fTexTop, vectorScale);
            //Top Mid Vertex 
            stVertexAtrribute tTopMidVertex = GenerateVertex(iX, iTopZ, fX, fTopZ, fTexMidX, fTexTop, vectorScale);
            //Top Right Vertex 
            stVertexAtrribute tTopRightVertex = GenerateVertex(iRightX, iTopZ, fRightX, fTopZ, fTexRight, fTexTop, vectorScale);
            //Right Mid Vertex 
            stVertexAtrribute tRightMidVertex = GenerateVertex(iRightX, iZ, fRightX, fZ, fTexRight, fTexMidZ, vectorScale);
            //Bottom Right Vertex 
            stVertexAtrribute tBottomRightVertex = GenerateVertex(iRightX, iBottomZ, fRightX, fBottomZ, fTexRight, fTexBottom, vectorScale);
            //Bottom Mide Vertex 
            stVertexAtrribute tBottomMidVertex = GenerateVertex(iX, iBottomZ, fX, fBottomZ, fTexMidX, fTexBottom, vectorScale);

            stFanGenerator tFanGenerator = new stFanGenerator(
                bDrawLeftMidVertex,
                bDrawTopMidVertex,
                bDrawRightMidVertex,
                bDrawBottomMidVertex,
                tCenterVertex,
                tBottomLeftVertex,
                tLeftMidVertex,
                tTopLeftVertex,
                tTopMidVertex,
                tTopRightVertex,
                tRightMidVertex,
                tBottomRightVertex,
                tBottomMidVertex
                ); 
                
            if ( curNode.mbSubdivide )
            {
                #region 已经是最小的LOD
                
                //已经是最小的LOD的
                if( curNodeLength <= 3 )
                {
                    tFanGenerator.DrawFan(ref meshData, enFanPosition.One);
                    tFanGenerator.DrawFan(ref meshData, enFanPosition.Two);
                    tFanGenerator.DrawFan(ref meshData, enFanPosition.Four);
                    tFanGenerator.DrawFan(ref meshData, enFanPosition.Eight);
                } // <= 3 

                #endregion

                #region 还可以继续划分

                else
                {
                    int tChildHalfLength = (curNodeLength - 1) >> 2;
                    float fChildHalfLength = (float)tChildHalfLength;

                    int tChildNodeLength = (curNodeLength + 1) >> 1;

                    int tFanCode = 0;


                    int iChildRightX = iX + tChildHalfLength;
                    int iChildTopZ = iZ + tChildHalfLength;
                    int iChildLeftX = iX - tChildHalfLength;
                    int iChildBottomZ = iZ - tChildHalfLength;

                    float fChildRightX = fX + fChildHalfLength;
                    float fChildTopZ = fZ + fChildHalfLength;
                    float fChildLeftX = fX - fChildHalfLength;
                    float fChildBottomZ = fZ - fChildHalfLength;

                    CQuadTreeNode topRightChildNode = GetNode( iChildRightX,iChildTopZ);
                    CQuadTreeNode topLeftChildNode = GetNode(iChildLeftX, iChildTopZ);
                    CQuadTreeNode bottomLeftChildNode = GetNode(iChildLeftX, iChildBottomZ);
                    CQuadTreeNode bottomRightChildNode = GetNode(iChildRightX, iChildBottomZ);

                    //拆分程度不能相差2
                    //左上子结点
                    //bool bTopLeftChildDivide = CanNodeDivide(topLeftChildNode,leftNeighborNode,topNeighborNode);
                    bool bTopLeftChildDivide = topLeftChildNode != null && topLeftChildNode.mbSubdivide;
                    //右上子结点
                    //bool bTopRightChildDivide = CanNodeDivide(topRightChildNode,topNeighborNode,rightNeighborNode);
                    bool bTopRightChildDivide = topRightChildNode != null && topRightChildNode.mbSubdivide;
                    //右下
                    //bool bBottomRightChildDivide = CanNodeDivide(bottomRightChildNode,bottomNeighborNode,rightNeighborNode);
                    bool bBottomRightChildDivide = bottomRightChildNode != null && bottomRightChildNode.mbSubdivide;
                    //左下
                    //bool bBottomLeftChildDivide = CanNodeDivide(bottomLeftChildNode,bottomNeighborNode,leftNeighborNode);
                    bool bBottomLeftChildDivide = bottomLeftChildNode != null && bottomLeftChildNode.mbSubdivide; 

                    //top right sud divide
                    if ( bTopRightChildDivide )
                    {
                        tFanCode |= 8;
                    }

                    //top left
                    if ( bTopLeftChildDivide )
                    {
                        tFanCode |= 4;
                    }

                    //bottom left 
                    if (bBottomLeftChildDivide)
                    {
                        tFanCode |= 2;
                    }

                    //bottom right 
                    if ( bBottomRightChildDivide )
                    {
                        tFanCode |= 1;
                    }

                    #region 各种情况的组合 

                    enNodeTriFanType fanType = (enNodeTriFanType)tFanCode;
                    switch (fanType)
                    {

                        #region 15 四个子结点都分割
                        //子结点一个都不分割
                        case enNodeTriFanType.No_Fan:
                            {
                                //bottom right 
                                RenderNode(fChildRightX, fChildBottomZ, tChildNodeLength, ref meshData, vectorScale);

                                //bottom left 
                                RenderNode( fChildLeftX ,fChildBottomZ, tChildNodeLength, ref meshData, vectorScale);

                                //top left 
                                RenderNode(fChildLeftX, fChildTopZ, tChildNodeLength, ref meshData, vectorScale);

                                //top right 
                                RenderNode(fChildRightX, fChildTopZ, tChildNodeLength, ref meshData, vectorScale);

                                break;
                            }
                        #endregion

                        #region  5 左上右下分割，左下右上画三角形
                        //左上右下分割，左下右上画三角形
                        case enNodeTriFanType.BottomLeft_TopRight:
                            {
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Two);
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Eight);

                                //bottom right 
                                RenderNode(fChildRightX, fChildBottomZ, tChildNodeLength, ref meshData, vectorScale);
                                //top left 
                                RenderNode(fChildLeftX, fChildTopZ, tChildNodeLength, ref meshData, vectorScale);

                                break;
                            }
                        #endregion

                        #region 10 左下右上分割，左上右下画三角形
                        case enNodeTriFanType.BottomRight_TopLeft:
                            {
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.One);
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Four);

                                //bottom left 
                                RenderNode(fX - fChildHalfLength, fZ - fChildHalfLength, tChildNodeLength, ref meshData, vectorScale);
                                //top right 
                                RenderNode(fX + fChildHalfLength, fZ + fChildHalfLength, tChildNodeLength, ref meshData, vectorScale);
                              
                                break;
                            }

                        #endregion

                        #region 0 直接画出8个三角形
                        case enNodeTriFanType.Complete_Fan:
                            {
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.One);
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Two);
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Four);
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Eight);

                                break;
                            }

                        #endregion

                        #region 1 左下左上右上划三角形
                        case enNodeTriFanType.BottomLeft_TopLeft_TopRight:
                            {
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Two);
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Four);
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Eight);

                                //Bottom Right Child Node
                                RenderNode(fChildRightX, fChildBottomZ, tChildNodeLength, ref meshData, vectorScale); 

                                break; 
                            }

                        #endregion

                        #region 2  左上右上右下划三角形

                        case enNodeTriFanType.TopLeft_TopRight_BottomRight:
                            {
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.One);
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Four);
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Eight);

                                //bottom left 
                                RenderNode(fChildLeftX, fChildBottomZ, tChildNodeLength, ref meshData, vectorScale);

                                break; 
                            }


                        #endregion

                        #region 3 左上右上划三角形

                        case enNodeTriFanType.TopLeft_TopRight:
                            {
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Four);
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Eight);

                                //bottom left 
                                RenderNode(fChildLeftX, fChildBottomZ, tChildNodeLength, ref meshData, vectorScale);
                                //bottom right 
                                RenderNode(fChildRightX, fChildBottomZ, tChildNodeLength, ref meshData, vectorScale);

                                break; 
                            }

                        #endregion

                        #region 4 右上右下左下划三角形
                        case enNodeTriFanType.TopRight_BottomRight_BottomLeft:
                            {
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.One);
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Two);
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Eight);

                                //top left 
                                RenderNode(fChildLeftX, fChildTopZ, tChildNodeLength, ref meshData, vectorScale);

                                break; 
                            }

                        #endregion

                        #region 6 右上右下划三角形
                        case enNodeTriFanType.TopRight_BottomRight:
                            {
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.One);
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Eight);

                                //bottom left 
                                RenderNode(fChildLeftX, fChildBottomZ, tChildNodeLength, ref meshData, vectorScale);

                                //top left 
                                RenderNode(fChildLeftX, fChildTopZ, tChildNodeLength, ref meshData, vectorScale);

                                break; 
                            }

                        #endregion

                        #region 7 右上划三角形
                        case enNodeTriFanType.TopRight:
                            {
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Eight);

                                //bottom right 
                                RenderNode(fChildRightX, fChildBottomZ, tChildNodeLength, ref meshData, vectorScale);
                                //bottom left 
                                RenderNode(fChildLeftX, fChildBottomZ, tChildNodeLength, ref meshData, vectorScale);
                                //top left 
                                RenderNode(fChildLeftX, fChildTopZ, tChildNodeLength, ref meshData, vectorScale);

                                break; 
                            }

                        #endregion

                        #region 8 右下左下左上划三角形
                        case enNodeTriFanType.BottomRight_BottomLeft_TopLeft:
                            {
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.One);
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Two);
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Four);
                                //top right 
                                RenderNode(fChildRightX, fChildTopZ, tChildNodeLength, ref meshData, vectorScale);

                                break; 
                            }
                        #endregion

                        #region 9 左下左上划三角形
                        case enNodeTriFanType.BottomLeft_TopLeft:
                            {
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Two);
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Four);

                                //bottom right 
                                RenderNode(fChildRightX, fChildBottomZ, tChildNodeLength, ref meshData, vectorScale);
                                //top right 
                                RenderNode(fChildRightX, fChildTopZ, tChildNodeLength, ref meshData, vectorScale);

                                break; 
                            }

                        #endregion

                        #region 11 左上划三角形
                        case enNodeTriFanType.TopLeft:
                            {
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Four);
                                //bottom right 
                                RenderNode(fChildRightX, fChildBottomZ, tChildNodeLength, ref meshData, vectorScale);
                                //bottom left 
                                RenderNode(fChildLeftX, fChildBottomZ, tChildNodeLength, ref meshData, vectorScale);
                                //top right 
                                RenderNode(fChildRightX, fChildTopZ, tChildNodeLength, ref meshData, vectorScale);

                                break; 
                            }

                        #endregion

                        #region 12 左下右下划三角形
                        case enNodeTriFanType.BottomLeft_BottomRight:
                            {
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.One);
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Two);

                                //top left 
                                RenderNode(fChildLeftX, fChildTopZ, tChildNodeLength, ref meshData, vectorScale);
                                //top right 
                                RenderNode(fChildRightX, fChildTopZ, tChildNodeLength, ref meshData, vectorScale);

                                break; 
                            }

                        #endregion

                        #region 13 左下划三角形
                        case enNodeTriFanType.BottomLeft:
                            {
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.Two);

                                //bottom right 
                                RenderNode(fChildRightX, fChildBottomZ, tChildNodeLength, ref meshData, vectorScale);

                                //top left 
                                RenderNode(fChildLeftX, fChildTopZ, tChildNodeLength, ref meshData, vectorScale);

                                //top right 
                                RenderNode(fChildRightX, fChildTopZ, tChildNodeLength, ref meshData, vectorScale);

                                break; 
                            }

                        #endregion

                        #region 14 右下划三角形
                        case enNodeTriFanType.BottomRight:
                            {
                                tFanGenerator.DrawFan(ref meshData, enFanPosition.One);
                                //bottom left 
                                RenderNode(fChildLeftX, fChildBottomZ, tChildNodeLength, ref meshData, vectorScale);
                                //top left 
                                RenderNode(fChildLeftX, fChildTopZ, tChildNodeLength, ref meshData, vectorScale);
                                //top right 
                                RenderNode(fChildRightX, fChildTopZ, tChildNodeLength, ref meshData, vectorScale);

                                break;
                            }

                            #endregion
                    }

                    #endregion

                }

                #endregion

            }  // if subdivied

        }  //RenderNode



        public void Render( ref stTerrainMeshData meshData ,Vector3 vertexScale )
        {
            Mesh mesh = meshData.mMesh; 
            if( null == mesh  )
            {
                Debug.LogError("Terrain without Mesh");
                return;
            }

            meshData.Reset(); 


            Profiler.BeginSample("Rebuild Vertices & UVs"); 
            Vector2[] uv = meshData.mUV;
            Vector3[] normals = meshData.mNormals; 
            Vector3[] vertices = meshData.mVertices ;
            for( int z = 0; z < mHeightData.mSize ; ++z)
            {
                for(int x = 0; x < mHeightData.mSize ; ++x)
                {
                    float y = mHeightData.GetRawHeightValue(x, z);
                    int vertexIdx = z * mHeightData.mSize + x; 
                    vertices[vertexIdx] = new Vector3(x*vertexScale.x, y * vertexScale.y, z * vertexScale.z);
                    uv[vertexIdx] = new Vector2((float)x / (float)mHeightData.mSize, (float)z / (float)mHeightData.mSize);
                    normals[vertexIdx] = Vector3.zero; 
                }
            }
            //mesh.vertices = vertices;
            //mesh.uv = uv;
            //mesh.normals = normals;
            Profiler.EndSample();


            Profiler.BeginSample("Rebuild Triangles");
            int nIdx = 0;
            int[] triangles = meshData.mTriangles; //一个正方形对应两个三角形，6个顶点
            for (int z = 0; z < mHeightData.mSize - 1; ++z)
            {
                for (int x = 0; x < mHeightData.mSize - 1; ++x)
                {
                    int bottomLeftIdx = z * mHeightData.mSize + x;
                    int topLeftIdx = (z + 1) * mHeightData.mSize + x;
                    int topRightIdx = topLeftIdx + 1;
                    int bottomRightIdx = bottomLeftIdx + 1;

                    triangles[nIdx++] = bottomLeftIdx;
                    triangles[nIdx++] = topLeftIdx;
                    triangles[nIdx++] = bottomRightIdx;
                    triangles[nIdx++] = topLeftIdx;
                    triangles[nIdx++] = topRightIdx;
                    triangles[nIdx++] = bottomRightIdx;

                }
            }

            //mesh.triangles = triangles;

            meshData.Present();

            Profiler.EndSample(); 
        }



        #endregion



        #region 地形纹理操作

        private List<CTerrainTile> mTerrainTiles = new List<CTerrainTile>();
        private Texture2D mTerrainTexture;
        public Texture2D TerrainTexture
        {
            get
            {
                return mTerrainTexture; 
            }

        }



        public void GenerateTextureMap( uint uiSize ,ushort maxHeight , ushort minHeight )
        {
            if( mTerrainTiles.Count <= 0 )
            {
                return; 
            }

            mTerrainTexture = null;
            int tHeightStride  = maxHeight / mTerrainTiles.Count ;

            float[] fBend = new float[mTerrainTiles.Count]; 

            //注意，这里的区域是互相重叠的
            int lastHeight = -1; 
            for(int i = 0; i < mTerrainTiles.Count; ++i)
            {
                CTerrainTile terrainTile = mTerrainTiles[i];
                //lastHeight += 1;
                terrainTile.lowHeight = lastHeight + 1 ;
                lastHeight += tHeightStride;

                terrainTile.optimalHeight = lastHeight;
                terrainTile.highHeight = (lastHeight - terrainTile.lowHeight) + lastHeight; 
            }

            for(int i = 0; i < mTerrainTiles.Count; ++i )
            {
                CTerrainTile terrainTile = mTerrainTiles[i];
                string log = string.Format("Tile Type:{0}|lowHeight:{1}|optimalHeight:{2}|highHeight:{3}",
                    terrainTile.TileType.ToString(),
                    terrainTile.lowHeight,
                    terrainTile.optimalHeight,
                    terrainTile.highHeight
                    ); 
                Debug.Log(log); 
            }



            mTerrainTexture = new Texture2D((int)uiSize,(int)uiSize, TextureFormat.RGBA32,false);

            CUtility.SetTextureReadble(mTerrainTexture, true);
          
            float fMapRatio = (float)mHeightData.mSize / uiSize;

            for (int z = 0; z < uiSize; ++z)
            {
                for (int x = 0; x < uiSize; ++x)
                {

                    Color totalColor = new Color();

                    for (int i = 0; i < mTerrainTiles.Count; ++i)
                    {
                        CTerrainTile tile = mTerrainTiles[i];
                        if (tile.mTileTexture == null)
                        {
                            continue;
                        }

                        int uiTexX = x;
                        int uiTexZ = z;

                        //CUtility.SetTextureReadble(tile.mTileTexture, true);

                        GetTexCoords(tile.mTileTexture, ref uiTexX, ref uiTexZ);

                       

                        Color color = tile.mTileTexture.GetPixel(uiTexX, uiTexZ);
                        fBend[i] = RegionPercent(tile.TileType, Limit(InterpolateHeight(x, z, fMapRatio),maxHeight,minHeight));

                        totalColor.r = Mathf.Min(color.r * fBend[i] + totalColor.r, 1.0f);
                        totalColor.g = Mathf.Min(color.g * fBend[i] + totalColor.g, 1.0f);
                        totalColor.b = Mathf.Min(color.b * fBend[i] + totalColor.b, 1.0f);
                        totalColor.a = 1.0f;

                        //CUtility.SetTextureReadble(tile.mTileTexture, false);
                    }// 

                    //输出到纹理上
                    if (totalColor.r == 0.0f 
                        && totalColor.g == 0.0f 
                        && totalColor.b == 0.0f)
                    {
                        ushort xHeight = (ushort)(x * fMapRatio);
                        ushort zHeight = (ushort)(z * fMapRatio); 
                        Debug.Log(string.Format("Color is Black | uiX:{0}|uiZ:{1}|hX:{2}|hZ:{3}|h:{4}",x,z,xHeight,zHeight,GetTrueHeightAtPoint(xHeight,zHeight))); 
                    }

                    mTerrainTexture.SetPixel(x, z,totalColor); 
                }
            }

            //OpenGL纹理的操作
            mTerrainTexture.Apply(); 
            CUtility.SetTextureReadble(mTerrainTexture, false);

            //string filePath = string.Format("{0}/{1}", Application.dataPath, "Runtime_TerrainTexture.png"); 
            //File.WriteAllBytes(filePath,mTerrainTexture.EncodeToPNG());
            //AssetDatabase.ImportAsset(filePath, ImportAssetOptions.ForceSynchronousImport | ImportAssetOptions.ForceUpdate);
            //AssetDatabase.SaveAssets();
        } // 


        public CTerrainTile GetTile( enTileTypes tileType )
        {
            return mTerrainTiles.Count > 0  ? mTerrainTiles.Find(curTile => curTile.TileType == tileType) : null; 
        }


        float RegionPercent( enTileTypes tileType , ushort usHeight  )
        {
            CTerrainTile tile = GetTile(tileType);
            if (tile == null)
            {
                Debug.LogError(string.Format("No tileType : Type:{0}|Height:{1}", tileType.ToString(), usHeight));
                return 0.0f ;
            }

            CTerrainTile lowestTile = GetTile(enTileTypes.lowest_tile);
            CTerrainTile lowTile = GetTile(enTileTypes.low_tile);
            CTerrainTile highTile = GetTile(enTileTypes.high_tile);
            CTerrainTile highestTile = GetTile(enTileTypes.highest_tile);

            //如果最低的块已经加载了，且落在它的low Height的块里面
            if (lowestTile != null  )
            {
                if( tileType == enTileTypes.lowest_tile 
                    && IsHeightAllLocateInTile(tileType,usHeight)) 
                {
                    return 1.0f; 
                }
            }

            else if (lowTile != null)
            {
                if (tileType == enTileTypes.low_tile
                    && IsHeightAllLocateInTile(tileType, usHeight))
                {
                    return 1.0f;
                }
            }
            else if ( highTile!= null)
            {
                if (tileType == enTileTypes.high_tile
                    && IsHeightAllLocateInTile(tileType, usHeight))
                {
                    return 1.0f;
                }
            }
            else if (highestTile != null)
            {
                if (tileType == enTileTypes.highest_tile
                    && IsHeightAllLocateInTile(tileType, usHeight))
                {
                    return 1.0f;
                }
            }

            //以[,)左闭右开吧
            if (usHeight < tile.lowHeight || usHeight > tile.highHeight)
            {
                return 0.0f;
            }


            if ( usHeight < tile.optimalHeight )
            {
                float fTemp1 = usHeight - tile.lowHeight;
                float fTemp2 = tile.optimalHeight - tile.lowHeight;

                //这段会产生小斑点，因为有些值可能会比较特殊
                //if (fTemp1 == 0.0f)
                //{
                //    Debug.LogError(string.Format("Lower than Optimal Height: Type:{0}|Height:{1}|fTemp1:{2}|lowHeight:{3}|optimalHeight:{4}", tileType.ToString(), usHeight, fTemp1, tile.lowHeight, tile.optimalHeight));
                //    return 1.0f;
                //}
                return fTemp1 / fTemp2; 
            }
            else if( usHeight == tile.optimalHeight )
            {
                return 1.0f; 
            }
            else if( usHeight > tile.optimalHeight  )
            {
                float fTemp1 = tile.highHeight - tile.optimalHeight;

                //这段会产生小斑点，因为有些值可能会比较特殊
                //if (((fTemp1 - (usHeight - tile.optimalHeight)) / fTemp1) == 0.0f)
                //{
                //    Debug.LogError(string.Format("Higher than Optimal Height: Type:{0}|Height:{1}|fTemp1:{2}|optimalHeight:{3}", tileType.ToString(), usHeight, fTemp1, tile.optimalHeight));
                //    return 1.0f;
                //}
                return ((fTemp1 - (usHeight - tile.optimalHeight)) / fTemp1);
            }

            Debug.LogError(string.Format("Unknow: Type:{0}|Height:{1}", tileType.ToString(), usHeight));
            return 0.0f; 
        }


        private ushort GetTrueHeightAtPoint( int x ,int z )
        {
            return mHeightData.GetRawHeightValue(x, z);
        }

        //两个高度点之间的插值，这里的很有意思的
        private ushort InterpolateHeight( int x,int z , float fHeight2TexRatio )
        {
            float fScaledX = x * fHeight2TexRatio;
            float fScaledZ = z * fHeight2TexRatio;

            ushort usHighX = 0;
            ushort usHighZ = 0; 

            //X的A点
            ushort usLow = GetTrueHeightAtPoint((int)fScaledX, (int)fScaledZ);

            if( ( fScaledX + 1 ) > mHeightData.mSize )
            {
                return usLow; 
            }
            else
            {
                //X的B点
                usHighX = GetTrueHeightAtPoint((int)fScaledX + 1, (int)fScaledZ);    
            }

            //X的A、B两点之间插值
            float fInterpolation = (fScaledX - (int)fScaledX);
            float usX = (usHighX - usLow) * fInterpolation + usLow;   //插值出真正的高度值 


            //Z轴同理
            if ((fScaledZ + 1) > mHeightData.mSize)
            {
                return usLow;
            }
            else
            {
                //X的B点
                usHighZ = GetTrueHeightAtPoint((int)fScaledX, (int)fScaledZ + 1);
            }

            fInterpolation = (fScaledZ - (int)fScaledZ);
            float usZ = (usHighZ - usLow) * fInterpolation + usLow;   //插值出真正的高度值

            return ((ushort)((usX + usZ) / 2));
        }

        private ushort Limit( ushort usValue , ushort maxHeight , ushort minHeight )
        {
            if( usValue > maxHeight )
            {
                return maxHeight ; 
            }
            else if( usValue < minHeight )
            {
                return minHeight; 
            }
            return usValue; 
        }


        private bool IsHeightAllLocateInTile( enTileTypes tileType , ushort usHeight )
        {
            bool bRet = false;
            CTerrainTile tile = GetTile(tileType);
            if (tile != null
                && usHeight <= tile.optimalHeight)
            {
                bRet = true; 
            }
            return bRet; 
        }


        //因为要渲染出来的一张地形纹理，可能会比tile的宽高都要大，所以要tile其实是平铺布满地形纹理的
        public void GetTexCoords( Texture2D texture , ref int x , ref int y)
        {
            int uiWidth = texture.width;
            int uiHeight = texture.height;

            int tRepeatX = -1;
            int tRepeatY = -1;
            int i = 0; 

            while( tRepeatX == -1 )
            {
                i++; 
                if( x < (uiWidth * i))
                {
                    tRepeatX = i - 1; 
                }
            }


            i = 0; 
            while( tRepeatY == -1 )
            {
                ++i; 
                if( y < ( uiHeight * i) )
                {
                    tRepeatY = i - 1; 
                }
            }


            x = x - (uiWidth * tRepeatX);
            y = y - (uiHeight * tRepeatY); 
        }




        public void AddTile( enTileTypes tileType , Texture2D tileTexture ) 
        {
            if( tileTexture != null  )
            {
                if( mTerrainTiles.Exists( curTile => curTile.TileType == tileType )) 
                {
                    CTerrainTile oldTile = mTerrainTiles.Find(curTile => curTile.TileType == tileType); 
                    if( oldTile != null )
                    {
                        oldTile = new CTerrainTile(tileType, tileTexture);  
                    }      
                }
                else
                {
                    mTerrainTiles.Add(new CTerrainTile(tileType, tileTexture)); 
                }
            }
        }


        #endregion

        #region 高度图数据操作

        private stHeightData mHeightData;

        public void UnloadHeightMap()
        {
            mHeightData.Release();

            Debug.Log("Height Map is Unload!"); 
        }


      



        /// <summary>
        /// This fuction came from book 《Focus On 3D Terrain Programming》 ,thanks Trent Polack a lot
        /// </summary>
        /// <param name="size"></param>
        /// <param name="iter"></param>
        /// <param name="minHeightValue"></param>
        /// <param name="maxHeightValue"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool MakeTerrainFault( int size , int iter , ushort minHeightValue , ushort maxHeightValue , float fFilter )
        {
            if( mHeightData.IsValid() )
            {
                UnloadHeightMap();      
            }

            mHeightData.Allocate(size);

            float[,] fTempHeightData = new float[size, size]; 

            for( int iCurIter = 0; iCurIter < iter; ++iCurIter )
            {
                //高度递减
                int tHeight = maxHeightValue - ((maxHeightValue - minHeightValue ) * iCurIter) / iter;
                //int tHeight = Random.Range(minHeightValue , maxHeightValue);  //temp 

                int tRandomX1 = Random.Range(0, size);
                int tRandomZ1 = Random.Range(0, size);

                int tRandomX2 = 0;
                int tRandomZ2 = 0;
                do
                {
                    tRandomX2 = Random.Range(0, size);
                    tRandomZ2 = Random.Range(0, size);        
                } while ( tRandomX2 == tRandomX1 && tRandomZ2 == tRandomZ1 );


                //两个方向的矢量
                int tDirX1 = tRandomX2 - tRandomX1;
                int tDirZ1 = tRandomZ2 - tRandomZ1; 

                //遍历每个顶点，看看分布在分割线的哪一边
                for( int z = 0; z < size; ++z)
                {
                    for(int x = 0; x < size; ++x )
                    {
                        int tDirX2 = x - tRandomX1;
                        int tDirZ2 = z - tRandomZ1; 
                        
                        if( (tDirX2 * tDirZ1 - tDirX1 * tDirZ2) > 0  )
                        {
                            fTempHeightData[x, z] += tHeight; //!!!!!自加符号有问题！！！！
                        }       
                    }
                }

                FilterHeightField(ref fTempHeightData,size,fFilter);

            }

            NormalizeTerrain(ref fTempHeightData, size , maxHeightValue ); 

            for(int z = 0; z < size; ++z)
            {
                for(int x = 0; x < size; ++x)
                {
                    SetHeightAtPoint((ushort)fTempHeightData[x, z], x, z);
                }
            }

            return true  ; 
        }


        void SetHeightAtPoint( ushort usHeight , int x, int z)
        {
            mHeightData.SetHeightValue(usHeight, x, z);
        }


        void NormalizeTerrain(ref float[,] fHeightData, int size , ushort maxHeight )
        {
            float fMin = fHeightData[0, 0];
            float fMax = fHeightData[0, 0];

            for (int z = 0; z < size; ++z)
            {
                for (int x = 0; x < size; ++x )
                {
                    if( fHeightData[x,z] > fMax)
                    {
                        fMax = fHeightData[x, z]; 
                    }

                    if(fHeightData[x,z] < fMin)
                    {
                        fMin = fHeightData[x, z]; 
                    }

                }   
            }

            Debug.Log(string.Format("Before Normailzed MaxHeight:{0}|MinHeight:{1}",fMax,fMin)); 

            if(fMax <= fMin)
            {
                return; 
            }

            float fHeight = fMax - fMin;
            for (int z = 0; z < size; ++z)
            {
                for (int x = 0; x < size; ++x)
                {
                    fHeightData[x, z] = ((fHeightData[x, z] - fMin) / fHeight) * maxHeight  ;
                }
            }


            ///////////////打LOG用
            fMax = fHeightData[0, 0];
            fMin = fHeightData[0, 0];
            for (int z = 0; z < size; ++z)
            {
                for (int x = 0; x < size; ++x)
                {
                    if (fHeightData[x, z] > fMax)
                    {
                        fMax = fHeightData[x, z];
                    }

                    if (fHeightData[x, z] < fMin)
                    {
                        fMin = fHeightData[x, z];
                    }
                }
            }

            Debug.Log(string.Format("After Normailzed MaxHeight:{0}|MinHeight:{1}", fMax, fMin));

            ///////////////////////////
        }

        void FilterHeightField( ref float[,] fHeightData ,int size , float fFilter )
        {
            //四向模糊

            //从左往右的模糊
            for ( int i = 0; i < size; ++i)
            {
                FilterHeightBand(ref fHeightData,
                    i,0,  //初始的x,y
                    0,1,       //数组步进值
                    size,      //数组个数
                    fFilter);
            }

            //从右往左的模糊
            for( int i = 0; i < size; ++i)
            {
                FilterHeightBand(ref fHeightData,
                    i, size -1,
                    0,-1,
                    size,
                    fFilter); 
            }


            //从上到下的模糊
            for (int i = 0; i < size; ++i)
            {
                FilterHeightBand(ref fHeightData,
                    0,i,
                    1,0,
                    size,
                    fFilter);
            }


            //从下到上的模糊
            for (int i = 0; i < size; ++i)
            {
                FilterHeightBand(ref fHeightData,
                    size - 1,i ,
                    -1,0,
                    size,
                    fFilter);
            }
        }



        void FilterHeightBand(
            ref float[,] fBandData ,
            int beginX,
            int beginY,
            int strideX,
            int strideY,
            int count , 
            float fFilter )
        {
            //Debug.Log(string.Format("BeginX:{0} | BeginY:{1} | StrideX:{2} | StrideY:{3}",beginX,beginY,strideX,strideY)); 
       
            //float beginValue = fBandData[beginX, beginY];
            float curValue = fBandData[beginX,beginY];
            int jx = strideX;
            int jy = strideY;

            //float delta = fFilter / (count - 1);
            for( int i = 0; i < count - 1; ++i)
            {
                int nextX = beginX + jx;
                int nextY = beginY + jy;

                fBandData[nextX, nextY] = fFilter * curValue + (1 - fFilter) * fBandData[nextX, nextY];
                curValue = fBandData[nextX, nextY];

                //float tFilter = fFilter - delta * ((jx - beginX + jy - beginY) * 0.5f);
                //fBandData[nextX, nextY] = tFilter * beginValue;

                jx += strideX;
                jy += strideY; 
            }
        }


        #endregion

        #region 粗糙度传播
        private stRoughnessData mRoughnessData;

        public void MakeRounessData(int size)
        {
            mRoughnessData.Allocate(size);
        }


        #endregion

    }
}
