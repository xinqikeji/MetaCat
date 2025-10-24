using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using UnityEngine.UI;
using Observer;
using DG.Tweening;

namespace MergeBeast {
    public class AscendWheelScr : MonoBehaviour {
        [SerializeField] private List<AscendRewardItem> _listReward;
        [SerializeField] private Button _btnWheel;
        [SerializeField] private Button _btnClose;
        [SerializeField] private Image _imgIconReward;
        [SerializeField] private Text _txtReward;
        [SerializeField] private Animator _animWheel;

        [SerializeField] private Text _txtTooltip;
        [SerializeField] AscendWheelReward reward;


        private List<int> _listPostCanReward;
        private int _cacheIndexReward;

        private Coroutine introWheel;
        // Start is called before the first frame update
        void Awake() {
            //_listPostCanReward = new List<int>() { PlayerPrefs.GetInt(StringDefine.ASCEND_REWARD, 1) };
            _listPostCanReward = new List<int>() { 1, 2, 3, 6, 9, 14 };
            _btnWheel?.onClick.AddListener(this.OnClickWheel);
            _btnClose?.onClick.AddListener(this.OnClickHidePopup);
        }

        private void OnEnable() {
            _btnWheel.interactable = true;
            int lvMonster = PlayerPrefs.GetInt(StringDefine.OLD_LEVEL_ASCEND, 0);
            int ran = Random.Range(0, _listPostCanReward.Count);
            _cacheIndexReward = _listPostCanReward[ran];

            for (int i = 0; i < _listReward.Count; i++) {
                _listReward[i].ResetState();
                _listReward[i]._showToolTip = ShowToolTip;
                _listReward[i]._hideTooltip = CloseToolTip;

                switch (i) {
                    case 1:
                    BigInteger soul = BigInteger.Pow(2, lvMonster - 10);
                    string sl = Utils.FormatNumber(soul);
                    _listReward[i].TxtSl.text = $"x{sl}";
                    _listReward[i].SetDescription($"Received {sl} Soul");

                    if (_cacheIndexReward == 1) {
                        UIManager.Instance.UpdateMoneyCoin(soul, false);
                        _imgIconReward.sprite = _listReward[i].transform.GetChild(3).GetComponent<Image>().sprite;
                        _txtReward.text = $"x{sl}";
                    }
                    break;
                    case 0:
                    _listReward[i].SetDescription($"Received x2 DPS in {lvMonster / 5} min.");
                    break;
                    case 2:
                    int star = 30 * lvMonster / 5;
                    _listReward[i].TxtSl.text = $"x{star}";
                    _listReward[i].SetDescription($"Received {star} star.");

                    if (_cacheIndexReward == 2) {
                        UIManager.Instance.AddMoneyMerge(star);
                        _imgIconReward.sprite = _listReward[i].transform.GetChild(3).GetComponent<Image>().sprite;
                        _txtReward.text = $"x{star}";
                    }
                    break;
                    case 3:
                    int gem = 2 * lvMonster / 5;
                    _listReward[i].TxtSl.text = $"x{gem}";
                    _listReward[i].SetDescription($"Received {gem} Gems.");
                    if (_cacheIndexReward == 3) {
                        Utils.AddRubyCoin(gem);
                        this.PostEvent(EventID.OnUpDateMoney);
                        _imgIconReward.sprite = _listReward[i].transform.GetChild(3).GetComponent<Image>().sprite;
                        _txtReward.text = $"x{gem}";
                    }
                    break;
                    case 4:
                    _listReward[i].SetDescription($"Received Time Jump in 2h.");
                    break;
                    case 5:
                    _listReward[i].SetDescription($"Received Time Jump in 8h.");
                    break;
                    case 6:
                    BigInteger sol = BigInteger.Pow(2, lvMonster - 12);
                    string slu = Utils.FormatNumber(sol);
                    _listReward[i].TxtSl.text = $"x{slu}";
                    _listReward[i].SetDescription($"Received {slu} Soul");

                    if (_cacheIndexReward == 6) {
                        UIManager.Instance.UpdateMoneyCoin(sol, false);
                        _imgIconReward.sprite = _listReward[i].transform.GetChild(3).GetComponent<Image>().sprite;
                        _txtReward.text = $"x{sol}";
                    }
                    break;
                    case 7:
                    _listReward[i].SetDescription($"Received x5 DPS in {lvMonster / 10} min.");
                    break;
                    case 8:
                    _listReward[i].SetDescription($"Received Offline Speed in {lvMonster}min.");
                    break;
                    case 9:
                    int slMerge = 10 * lvMonster / 5;
                    _listReward[i].TxtSl.text = $"x{slMerge}";
                    _listReward[i].SetDescription($"Received {slMerge} Medal Merge.");
                    if (_cacheIndexReward == 9) {
                        BoostManager.Instance?.OnBoostMedalMerge(slMerge);
                        _imgIconReward.sprite = _listReward[i].transform.GetChild(3).GetComponent<Image>().sprite;
                        _txtReward.text = $"x{slMerge}";
                    }
                    break;
                    case 10:
                    _listReward[i].SetDescription($"Received x2 Ascend");
                    break;
                    case 11:
                    _listReward[i].SetDescription($"Received Time Jump in 6h.");
                    break;
                    case 12:
                    _listReward[i].SetDescription($"Received Spawn Faster in {lvMonster / 5} min.");
                    break;
                    case 13:
                    _listReward[i].SetDescription($"Received Boost Auto Merge in {lvMonster / 5} min.");
                    break;
                    case 14:
                    int gems = lvMonster / 5;
                    _listReward[i].TxtSl.text = $"x{gems}";
                    _listReward[i].SetDescription($"Received {gems} Gems.");
                    if (_cacheIndexReward == 14) {
                        Utils.AddRubyCoin(gems);
                        UIManager.Instance.UpdateMoneyRuby();
                        _imgIconReward.sprite = _listReward[i].transform.GetChild(3).GetComponent<Image>().sprite;
                        _txtReward.text = $"x{gems}";
                    }
                    break;
                    case 15:
                    _listReward[i].SetDescription($"Received Boost Spawn Higher Level");
                    break;
                    default: break;
                }
            }
            _animWheel.Play("infoAscend-show");
            _isIDLE = true;
            if (introWheel == null) {
                introWheel = StartCoroutine(IEWheelIDLE());
            }
        }

        private void OnClickWheel() {
            StartCoroutine(IEWheel());
            _btnWheel.interactable = false;
        }

        private void OnClickHidePopup() {
            _animWheel.Play("infoAscend-hide");
            StartCoroutine(IEHidelPopup());
        }

        private IEnumerator IEHidelPopup() {
            yield return new WaitForSeconds(0.4f);
            reward.gameObject.SetActive(true);
            this.gameObject.SetActive(false);

        }

        private bool _isIDLE;
        private IEnumerator IEWheelIDLE() {
            while (_isIDLE) {
                for (int i = 0; i < _listReward.Count; i++) {
                    _listReward[i].ImgSelected.color = Color.white;

                    if (i == 0) _listReward[_listReward.Count - 1].ResetState();
                    else _listReward[i - 1].ResetState();

                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        private IEnumerator IEWheel() {
            _isIDLE = false;
            if (introWheel != null) StopCoroutine(introWheel);
            int wheelCount = Random.Range(1, 4) * _listReward.Count + _cacheIndexReward;
            int count = 0;

            while (count <= wheelCount) {
                int mainIndex = count % _listReward.Count;
                for (int i = 0; i < _listReward.Count; i++) {
                    int idex = ((_listReward.Count + mainIndex - i) % _listReward.Count);
                    if (count - i >= 0 && i < 5) {

                        _listReward[idex].ImgLight.color = _listReward[idex].ImgLight2.color = new Color(1f, 1f, 1f, 1f - i * 0.25f);
                        _listReward[idex].ImgSelected.color = new Color(1f, 1f, 1f, 1f - 0.1f * i);
                    } else {
                        _listReward[idex].ResetState();
                    }
                }
                count++;
                yield return new WaitForSeconds(0.05f);
            }

            for (int i = 4; i > 0; i--) {
                int idex = ((_listReward.Count + _cacheIndexReward - i) % _listReward.Count);
                _listReward[idex].ResetState();

                yield return new WaitForSeconds(0.05f);
            }

            _listReward[_cacheIndexReward].SetSelected();

            yield return new WaitForSeconds(1.3f);
            reward.gameObject.SetActive(true);
        }


        // Update is called once per frame


        private void ShowToolTip(string des) {
            _txtTooltip.transform.parent.gameObject.SetActive(true);
            _txtTooltip.text = des;
        }
        private void CloseToolTip() {
            _txtTooltip.transform.parent.gameObject.SetActive(false);
        }
    }
}
