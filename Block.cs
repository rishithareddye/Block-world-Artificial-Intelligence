using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMoving
{
    class Block
    {
        public int x, y,posx,posy;
        public Block(int pos,int height)
        {
            posx = pos;
            posy = height;
            x = 50;
            for(int i =0;i<pos;i++)
            {
                x += 300;
            }
            y = 400;
            for (int i = 0; i < height; i++)
            {
                y -= 50;
            }
        }
        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }
        public void Reinistiate(int pos,int height)
        {
            posx = pos;
            posy = height;
            x = 50;
            for (int i = 0; i < pos; i++)
            {
                x += 300;
            }
            y = 400;
            for (int i = 0; i < height; i++)
            {
                y -= 50;
            }
        }
       
    }
}
