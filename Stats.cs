using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

	IBehaviour behaviourScript;

	[SerializeField]
	float _stamina = 100;
	float resultStamina;
	float addstamina;

	[SerializeField]
	 float _health =100;
	float resultHealth;
	float addhealth;

	[SerializeField]
	float _strength;
	float resultStrength;
	float addstrength;

	[SerializeField]
	float _dexterity;
	float resultDexterity;
	float adddexterity;

	[SerializeField]
	float _agility;
	float resultAgility;
	float addagility;

	[SerializeField]
	 float _shield;
	float resultShield;
	float addshield;

	[SerializeField]
	 float _humanity;
	float resultHumanity;
	float addhumanity;

	[SerializeField]
	float _goodness;
	float resultGoodness;
	float addgoodness;

	[SerializeField]
	 float _authority;
	float resultAuthority;
	float addauthority;


	[SerializeField]
	 float _size;
	float addsize;
	float resultSize;

	[SerializeField]
	public bool _gender;


	[SerializeField]
	float _fecundity;
	float resultFecundity;
	float addfecundity;


	[SerializeField]
	float _hunger;
	float resultHunger;
	float addhunger;

	[SerializeField]
	float _sleepiness;
	float resultSleepiness;
	float addsleepiness;

	[SerializeField]
	float _anger;
	float resultAnger;
	float addanger;

	[SerializeField]
	float _laziness;
	float resultLaziness;
	float addlaziness;

	[SerializeField]
	float _speed;
	float resultSpeed;
	float addspeed;


	[SerializeField]
	float _intelligence;
	float resultIntelligence;
	float addintelligence;


	public float intelligence
	{
		get{ return resultIntelligence=_intelligence+addintelligence; }
		set{ _intelligence = value; }
	}

	public float speed
	{
		get{ return resultSpeed=agility/10+addspeed; }
	
	}
	public float agility
	{
		get{ return resultAgility=_agility+addagility; }
		set{ _agility = value; }
	}

	public float dexterity
	{
		get{ return resultDexterity=_dexterity+adddexterity; }
		set{ _dexterity = value; }
	}
	public float health
	{
		get{ return resultHealth=_health+addhealth; }
		set{ _health = value; }
	}
	public float stamina
	{
		get{ return resultStamina=_stamina+addstamina; }
		set{ _stamina = value; }
	}
	public float authority
	{
		get{ return resultAuthority=_authority+addauthority; }
		set{ _authority = value; }
	}
	public float strength{
		get{
			return resultStrength=_strength+addstrength;
		}
		set{ _strength = value; }

	}

	public float humanity
	{
		get{ return resultHumanity=_humanity+addhumanity; }
		set{ _humanity = value; }
	}
	public float goodness
	{
		get{ return resultGoodness=_goodness+addgoodness; }
		set{ _goodness = value; }
	}

	public 	float shield{
		get{ return resultShield=_shield+addshield; }
		set{ _shield = value; }

	
	}

	public float size{
		get{
			return resultSize=_size+addsize;
		}
	}

	public bool gender
	{
		get{return _gender;
		}

	}


	public float fecundity{
		get{ return resultFecundity=_fecundity+addfecundity; }
		set{ _fecundity = value; }
	}
	public float hunger
	{
		get{ return resultHunger=_hunger+addhunger; }
		set{ _hunger = value; }
	}
	public float sleepiness
	{
		get{ return resultSleepiness=_sleepiness+addsleepiness; }
		set{ _sleepiness = value; }
	}

	public float anger
	{
		get{ return resultAnger=_anger+addanger; }
		set{ _anger = value; }
	}
		
	public float laziness
	{
		get{ return resultLaziness=_laziness+addlaziness; }
		set{ _laziness = value; }
     }




	float prevTime;

	Timer timer;


	void Start () {

		try{behaviourScript = GetComponent<IBehaviour>();
		}
		catch{
			behaviourScript = transform.root.GetComponent<IBehaviour> ();
		}
		try{_size = GetComponent<NavMeshAgent> ().height;
		}
		catch {
			_size = transform.lossyScale.magnitude;
		}
	
	}
	void CalculateStats()
	{
		resultStrength = _strength + addstrength;
		resultAuthority =_authority+ addauthority;
    resultSpeed = resultAgility / 10 + addspeed;

    }
	void Clamping()
	{
		_health = Mathf.Clamp (_health, 0, 100);
		_stamina = Mathf.Clamp (_stamina, 0, 100);
		_hunger = Mathf.Clamp (_hunger, 0, 100);
		_sleepiness = Mathf.Clamp (_sleepiness, 0, 100);
		_laziness = Mathf.Clamp (_laziness, 0, 100);
		_strength = Mathf.Clamp (_strength, 0, 100);
		_dexterity = Mathf.Clamp (_dexterity, 0, 100);
		_humanity = Mathf.Clamp (_humanity, 0, 100);
		_shield = Mathf.Clamp (_shield, 0, 100);
        _speed = Mathf.Clamp(_speed, 0, 30);
        _goodness = Mathf.Clamp (_goodness, 0, 100);
		_fecundity = Mathf.Clamp (_fecundity, 0, 100);
		_dexterity = Mathf.Clamp (_dexterity, 0, 100);
		_agility = Mathf.Clamp (_agility, 0, 100);
		_authority = Mathf.Clamp (_authority, 0, 100);

	}



	void FixedUpdate () {






		if (resultHealth <= 0) {

		}

		if ((Time.fixedTime - prevTime) > 5f) {
			prevTime = Time.fixedTime;
			_stamina += _strength/3 + dexterity / 2+(hunger/10-5f);
			_stamina = Mathf.Clamp (_stamina, 0, 100);

			_hunger -= 10f*size/stamina;
			_hunger = Mathf.Clamp (_hunger, 0, 100);
			if (hunger < 10) {
				
				_sleepiness -= 5f;
				_sleepiness = Mathf.Clamp (_sleepiness, 0, 40);
				addstrength = -15;
				addauthority -= 2f;
				addauthority = Mathf.Clamp (addauthority, 100, -40);
				if (hunger <= 0) {
					_health -= 0.5f;
					_health = Mathf.Clamp (_health, 10, 80);
				}
			} else if (hunger > 80) { 
				if (anger < 20)
					_sleepiness += 5f;
				_laziness +=5f;
			}
		
		}

	}//end Update
}
