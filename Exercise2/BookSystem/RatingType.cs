/*
 *  File Name: RatingType.cs
 *  Description: The type definitions of rating recommendation for the book.
 *  Version: 1.0
 *  
 *  Author: Youfang Yao
 *  Date: 2024-09-17
 * 
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem
{
    public enum RatingType
    {
        MustHave,
        Buy,
        Pass,
        EasyReading,
        Entertaining,
        SummerReading
    }
}
