using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Services.UI.Gameplay.EndGame.Views
{
    [RequireComponent(typeof(Canvas))]
    public class EndGameUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI endText;
        [field: SerializeField] public Button ExitToMenuButton { get; private set; }
        public void SetEndText(string text)
        {
            endText.text = text;
        }
        
        
    }
}