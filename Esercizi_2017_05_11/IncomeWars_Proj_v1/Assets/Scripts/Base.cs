using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Base : Target
{
	public Player editorOwner;

	public int StartMoney;
	public int StartIncome;

	public Text hpText;
	public Text moneyText;
	public Text incomeText;

	private int _currentMoney;
	private int _currentIncome;


	// Use this for initialization
	void Start () 
	{ 
		_currentMoney = StartMoney;
		_currentIncome = StartIncome;
		owner = editorOwner;
		UpdateHUD();
		StartCoroutine (IncreaseMoney ());
	}
		
	void UpdateHUD()
	{
		hpText.text = currentHP.ToString ();
		moneyText.text = _currentMoney.ToString ();
		incomeText.text = _currentIncome.ToString ();
	}
		
	protected override void Loss(int loss)
	{
		base.Loss(loss);
		UpdateHUD();
	}
		
	private IEnumerator IncreaseMoney ()
	{
		while (true)
		{
			yield return new WaitForSeconds (10f);
			_currentMoney += _currentIncome;
			UpdateHUD ();
		}
	}

	public void increaseIncome(int increment)
	{
		_currentIncome += increment;
		UpdateHUD ();
	}

	public void decreaseMoney(int cost)
	{
		_currentMoney -= cost;
		UpdateHUD ();
	}

	void OnDestroy()
	{
		gameController.GameOver(owner);
	}

	public int getMoney()
	{
		return _currentMoney;
	}

	public void redeemBounty(int bounty)
	{
		_currentMoney += bounty;
		UpdateHUD ();
	}
		
}
