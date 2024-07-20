


// video 15  karana edits tika

if(WeponManager.Instance.grenades <= 0)
{
    lethalUI.sprite = greySlot;

}
//soundMnager
public AudioSource throwablesChannel;
public AudioClip grenadeSound;

// grenade effect
//play sound
SoundManager.Instance.throwablesChannel1.PlayOneShot(SoundManager.Instance.grenadeSound);

//throwable reigion
PickupThrowablesAsLethal(Throwable.ThrowableType.Grenade);

//gameobject in weopon manager script
public int lethalsCount = 0;
public Throwable.ThrowableType equippedLethalType;

//class throwabletype in throwable script
None


//wepon manager script start method

    equippedLethalType = Throwable.ThrowableType.None;


//pickupthrowables as lethals method

if(equippedLethalType ==equippedLethalType || equipedLethalType == Throwable.ThrowableType.None)
{
    equippedLethalType = lethal;

    if(lethalsCount < 2)
    {
        lethalsCount += 1;
        Destroy(InteractionManager.Instance.hoveredThrowables.gameObjects);
        HUDManager.Instance.UpdateThrowables();
    }
    else
    {
        print("Lethals limit reacher");
    }

}

//hud manager script
 public void UpdateThrowablesUI()
{
    lethalAmount.text = $"{WeponManager.Instance.lethalsCount}";

    switch(WeponManager.Instance.equippedLethalType)

        Line ekk delete krnwa blpn eka

        //dont change others in this
}





//wepon manager

GameObject lethalPrefab = GetThrowablePrefab();

private GameObject GetThrowablePrefab()
{
    switch (equippedLethalType)
    {
        case Throwable.ThrowableType.Grenade:
            return grenadePrefab;
    }

    return new();

}



//throw lethal method
if(lethalsCount <= 0)
{
    equippedLethalType = Throwable.ThrowableType.Note;

}













// 16th video

        //adding enemies

//issellma layers hadana cn ekk thiyenne ek hdpn aa



