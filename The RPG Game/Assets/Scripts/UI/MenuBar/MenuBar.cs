using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBar : MonoBehaviour
{
    public Player player;

    public GameObject parent;
    public RectTransform background;
    public float height;

    public Interactable target;
    public Text targetText;

    public MenuButton menuButton;
    public GameObject nameText;

    #region singelton

    public static MenuBar instance;

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<Player>();

        if (instance != null) {
            Debug.LogWarning("Warning: More than one instance of MenuBat found!");
            return;
        }
        instance = this;

    }
    #endregion

    public void Start() {
        HideMenuBar();
    }

    public void Update() {
        MenuBarPosition();
        ToggleMenuBar();
    }


    private void ShowMenuBar(Interactable target) {
        if (!parent.gameObject.activeInHierarchy) {
            parent.gameObject.SetActive(true);
            GameObject textObject = Instantiate(nameText, background.transform);
            Text textArea = textObject.GetComponentInChildren<Text>();

            if (target.GetComponent<Construction>()) {
                string _name = target.GetComponent<Construction>().objectName;
                textArea.text = _name;

                GameObject but1 = Instantiate(menuButton.gameObject, background.transform);
                GameObject but2 = Instantiate(menuButton.gameObject, background.transform);
                GameObject but3 = Instantiate(menuButton.gameObject, background.transform);
                MenuButton button1 = but1.GetComponent<MenuButton>();
                MenuButton button2 = but2.GetComponent<MenuButton>();
                MenuButton button3 = but3.GetComponent<MenuButton>();

                button1.SetText("Interact");
                button1.button.onClick.AddListener(InteractButton);

                button2.SetText("Pick Up");
                button2.button.onClick.AddListener(PickUpObject);

                button3.SetText("Cancel");
                button3.button.onClick.AddListener(CancelButton);
            }

            else if (target.GetComponent<CutTree>()) {
                if (target.GetComponent<CollectItem>()) {
                    string _name = target.GetComponent<CutTree>().treeName;
                    textArea.text = _name;

                    GameObject but1 = Instantiate(menuButton.gameObject, background.transform);
                    GameObject but2 = Instantiate(menuButton.gameObject, background.transform);
                    GameObject but3 = Instantiate(menuButton.gameObject, background.transform);
                    MenuButton button1 = but1.GetComponent<MenuButton>();
                    MenuButton button2 = but2.GetComponent<MenuButton>();
                    MenuButton button3 = but3.GetComponent<MenuButton>();

                    button1.SetText("Cut Tree");
                    button1.button.onClick.AddListener(InteractButton);

                    button2.SetText("Pick Apple");
                    button2.button.onClick.AddListener(CollectItem);

                    button3.SetText("Cancel");
                    button3.button.onClick.AddListener(CancelButton);
                }
                else {
                    string _name = target.GetComponent<CutTree>().treeName;
                    textArea.text = _name;

                    GameObject but1 = Instantiate(menuButton.gameObject, background.transform);
                    GameObject but2 = Instantiate(menuButton.gameObject, background.transform);
                    MenuButton button1 = but1.GetComponent<MenuButton>();
                    MenuButton button2 = but2.GetComponent<MenuButton>();

                    button1.SetText("Cut Tree");
                    button1.button.onClick.AddListener(InteractButton);

                    button2.SetText("Cancel");
                    button2.button.onClick.AddListener(CancelButton);
                }

            }

            else if (target.GetComponent<MineRock>()) {
                string _name = target.GetComponent<MineRock>().rockName;
                textArea.text = _name;

                GameObject but1 = Instantiate(menuButton.gameObject, background.transform);
                GameObject but2 = Instantiate(menuButton.gameObject, background.transform);
                MenuButton button1 = but1.GetComponent<MenuButton>();
                MenuButton button2 = but2.GetComponent<MenuButton>();

                button1.SetText("Mine Rocks");
                button1.button.onClick.AddListener(InteractButton);

                button2.SetText("Cancel");
                button2.button.onClick.AddListener(CancelButton);

            }

            else if (target.GetComponent<FishingInLake>()) {
                string _name = target.GetComponent<FishingInLake>().lakeName;
                textArea.text = _name;

                GameObject but1 = Instantiate(menuButton.gameObject, background.transform);
                GameObject but2 = Instantiate(menuButton.gameObject, background.transform);
                MenuButton button1 = but1.GetComponent<MenuButton>();
                MenuButton button2 = but2.GetComponent<MenuButton>();

                button1.SetText("Fish");
                button1.button.onClick.AddListener(InteractButton);

                button2.SetText("Cancel");
                button2.button.onClick.AddListener(CancelButton);

            }

            else if (target.GetComponent<PickTwigs>()) {
                string _name = target.GetComponent<PickTwigs>()._name;
                textArea.text = _name;

                GameObject but1 = Instantiate(menuButton.gameObject, background.transform);
                GameObject but2 = Instantiate(menuButton.gameObject, background.transform);
                MenuButton button1 = but1.GetComponent<MenuButton>();
                MenuButton button2 = but2.GetComponent<MenuButton>();

                button1.SetText("Dismantle");
                button1.button.onClick.AddListener(InteractButton);

                button2.SetText("Cancel");
                button2.button.onClick.AddListener(CancelButton);
            }

            else if (target.GetComponent<RemoveStub>()) {
                string _name = target.GetComponent<RemoveStub>()._name;
                textArea.text = _name;

                GameObject but1 = Instantiate(menuButton.gameObject, background.transform);
                GameObject but2 = Instantiate(menuButton.gameObject, background.transform);
                MenuButton button1 = but1.GetComponent<MenuButton>();
                MenuButton button2 = but2.GetComponent<MenuButton>();

                button1.SetText("Remove");
                button1.button.onClick.AddListener(InteractButton);

                button2.SetText("Cancel");
                button2.button.onClick.AddListener(CancelButton);
            }

            else if (target.GetComponent<Enemy>()) {
                string _name = target.GetComponent<Enemy>().enemyName;
                textArea.text = _name;

                GameObject but1 = Instantiate(menuButton.gameObject, background.transform);
                GameObject but2 = Instantiate(menuButton.gameObject, background.transform);
                MenuButton button1 = but1.GetComponent<MenuButton>();
                MenuButton button2 = but2.GetComponent<MenuButton>();

                button1.SetText("Attack");
                button1.button.onClick.AddListener(InteractButton);

                button2.SetText("Cancel");
                button2.button.onClick.AddListener(CancelButton);
            }
        }
    }



    public void InteractButton() {
        player.SetFocus(target);
        target = null;
    }

    public void CancelButton() {
        target = null;
    }

    public void PickUpObject() {
        Construction construct = target as Construction;
        construct.isDoingAction = true;
        target = null;
    }

    public void CollectItem() {
        CollectItem collectItem = target.GetComponent<CollectItem>();
        collectItem.isDoingAction = true;
        target = null;
    }

    


    private void MenuBarPosition() {
        if(target != null) {
            Vector2 position = Camera.main.WorldToScreenPoint(target.transform.position);
            transform.position = position + new Vector2(0, 25);
        }
    }

    private void HideMenuBar() {
        parent.gameObject.SetActive(false);
        height = 0;
        if(background.childCount > 0) {
            for (int i = 0; i < background.childCount; i++) {
                Destroy(background.GetChild(i).gameObject);
            }
        }
    }

    private void ToggleMenuBar() {
        if(target != null) {
            ShowMenuBar(target);
        }
        else if(target == null) {
            HideMenuBar();
        }
    }

    private void SetTarget(Interactable target) {
        if(target == null) {
            this.target = null;
        }

        if(this.target == null) {
            this.target = target;
        }
        else if(this.target != null && target != null) {
            if (this.target.GetInstanceID() == target.GetInstanceID()) {
                this.target = null;
            }
            else if (this.target.GetInstanceID() != target.GetInstanceID()) {
                this.target = null;
            }
        }
    }

    public static void SetTarget_static(Interactable target) {
        instance.SetTarget(target);
    }








    /*
     target = MenuManager.menuTarget.GetComponent<Construction>();
        Vector2 position = Camera.main.WorldToScreenPoint(target.transform.position);
        background.transform.position = position + new Vector2(50, 50);
        targetText.text = target.GetComponent<Construction>().objectName;

            background.gameObject.SetActive(true);
            Vector2 backgroundSize = new Vector2(targetText.preferredWidth, targetText.preferredHeight + height);
            background.sizeDelta = backgroundSize;
    */



}
