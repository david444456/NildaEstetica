using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.Mobile
{
    public interface IRewardUITime 
    {
        void ChangeActiveStateRewardPoster(bool newValue);
        void ChangeTextCoinsRewardPosterTime(string newText);
    }
}
