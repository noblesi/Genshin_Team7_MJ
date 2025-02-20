using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RenderManager : MonoBehaviour
{
    public GameObject[] Manekins;
    public Material[] materials;
    public Material[] materialstexture;
    public RenderImage renderImage;
    MeshRenderer[] meshRenderers;
    public Transform barTrans_Parent;
    Transform barTrans;
    public PropertyText_Property propertyText;
    public PropertyText_Weapon weaponPropertyText;
    public PropertyText_Relic relicPropertyText;
    public ParticleSystem particle;

    public Sprite[] playerElement = new Sprite[4];

    public Image elementImage;
    public TextMeshProUGUI characterName;

    private void Awake()
    {
        Manekins = new GameObject[4]; // 마네킹 캐릭터 배열 초기화
        meshRenderers = new MeshRenderer[3]; // 매터리얼 바꿔야 하는 매쉬 랜더러 초기화 0 구배경, 1 구이펙트, 2, 바닥
        for (int i = 0; i < 4; i++)
        {
            Manekins[i] = transform.GetChild(0).GetChild(i).gameObject; // 마네킹 받아오기
            
        }

        //컴포넌트 받아오기
        meshRenderers[0] = transform.GetChild(2).GetComponent<MeshRenderer>();

        meshRenderers[1] = transform.GetChild(3).GetComponent<MeshRenderer>();
        meshRenderers[2] = transform.GetChild (4).GetComponent<MeshRenderer>();

        barTrans = barTrans_Parent.GetChild(4);
    }

    public void ChangeCharacter(bool right)
    {
        if (right)
        {
            int index = (int)EquipManager.Instance.character;
            ChangeCharacter((index + 1)%4);
        }
        else
        {
            int index = (int)EquipManager.Instance.character;
            ChangeCharacter((index + 3) % 4);
        }
    }

    public void ChangeCharacter(int index) // 캐릭터 패널에서 캐릭터 누를때 캐릭터 변경 
    {
        EquipManager.Instance.character = (CharacterItemSprite)index; //소환되어야 하는 캐릭터 설정
        try
        {
            propertyText.UpdatePanel(); // 정보 업데이트
            weaponPropertyText.UpdatePanel(); // 장비창 패널 해당 캐릭터로 업데이트
            relicPropertyText.UpdatePanel(); // 성유물창 업데이트
        }
        catch { }
        renderImage.touched = false; // 캐릭터 바뀔때 카메라 초기화

        for (int i = 0; i < Manekins.Length; i++)
        {
            if (i == index) // 해당 캐릭터일때 활성화랑 배경 변경
            {
              
                    barTrans.position = barTrans_Parent.GetChild(i).position;
                    Manekins[i].gameObject.SetActive(true);

                    meshRenderers[0].material = materials[i];
                    meshRenderers[1].material = materialstexture[i];
                    meshRenderers[2].material = materialstexture[i];

                    //파티클
                    var colorOverLifetime = particle.colorOverLifetime;
                    ParticleSystem.MinMaxGradient gradient = new ParticleSystem.MinMaxGradient(materials[i].color);
                    colorOverLifetime.color = gradient;            

            }
            else // 아니면 캐릭터 비활성화
            {
                Manekins[i].gameObject.SetActive(false);
            }
        }

        elementImage.sprite = playerElement[index];
        switch(index)
        {
            case 0:
                characterName.text = "번개 원소 / 북두";
                break;
            case 1:
                characterName.text = "물 원소 / 코코미";
                break;
            case 2:
                characterName.text = "얼음 원소 / 라이오슬리";
                break;
            case 3:
                characterName.text = "불 원소 / 요이미아";
                break;
        }

    }    

}
