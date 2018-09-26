package com.example.a201625221.projetjelly;

import android.content.res.ColorStateList;
import android.graphics.Color;
import android.support.constraint.ConstraintLayout;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Gravity;
import android.view.MotionEvent;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;

public class MainActivity extends AppCompatActivity {


    /**
     * Variables pour contenir les layouts pour pouvoir changer d'onglet dans l'application
     */
    ConstraintLayout listDrinkLYT,listIngLYT,optionsLYT,cartLYT,infosLYT,notesLYT;

    /**
     * Variables pour contenir les boutons pour pouvoir changer d'onglet dans l'application
     */
    Button drinkBTN,ingBTN,cartBTN,optionsBTN;

    /**
     * Variables permettant d'afficher dans la ListView les éléments des ArrayList<HashMap<String,String>> en passant par l'adapter
     */
    ListView listDrinkLVIEW,listIngLVIEW,cartLVIEW;

    /**
     * Tableaux pour indiquer l'origine des données de l'adapter
     */
    String from[]={"nom","desc","note"};
    /**
     * Tableau la destination graphique de l'adapter
     */
    int to[]={R.id.name_TXT,R.id.ing_TXT,R.id.note_TXT};

    /**
     * Listes contenant les éléments de la BD et le panier
     */
    ArrayList<HashMap<String,String>> arrayListDrink =new ArrayList<>(),arrayListIng=new ArrayList<>(),arrayListCart=new ArrayList<>();

    ArrayList<Integer>selectedCartPositions=new ArrayList<>();
    /**
     * Variable contenant l'objet selectionné dans le panier
     */

    String[] IngName ={"Orange juice","Ice","Salt","Water","Grenadine","Gold powder"};
    String[] DrinkName={"Sex on the beach","Cosmopolitan","Rhum and coke","Beer","Diesel","Water"};
    String[] DrinksIngredients={"Vodka+OrangeJuice+Grenadine","xxx","Rhum+Coke","Beer","Beer+Coke","Water"};
    String[] Notes={"1.9","2.8","5.7","3","2","1"};


    int compteurItemCourant;
    /**
     * Fonction lancée à la création de l'activité
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        Initialize();
    }
    /**
     * Appel de toutes les fonctions d'initialisation
     */
    void Initialize()
    {
        InitializeComponents();
        InitLists();
        setTouchListeners();
        setClickListeners();
    }

    /**
     * Initialise les composantes globales utilisées plusieurs fois pour ne pas avoir à les rechercher dans les fonctions
     */
     void InitializeComponents()
    {
        listDrinkLYT=findViewById(R.id.listDrink_LYT);
        listIngLYT=findViewById(R.id.listIng_LYT);
        optionsLYT=findViewById(R.id.options_LYT);
        cartLYT=findViewById(R.id.cart_LYT);
        infosLYT=findViewById(R.id.infos_LYT);
        notesLYT=findViewById(R.id.notes_LYT);

        drinkBTN=findViewById(R.id.drinklist_BTN);
        ingBTN=findViewById(R.id.inglist_BTN);
        cartBTN=findViewById(R.id.cart_BTN);
        optionsBTN=findViewById(R.id.options_BTN);

        listDrinkLVIEW=findViewById(R.id.drink_LVIEW);
        listIngLVIEW=findViewById(R.id.ing_LVIEW);
        cartLVIEW=findViewById(R.id.cart_LVIEW);
    }

    /**
     * Initialise les trois listes principales, puis les remplis à partir de la BD
     */
    void InitLists()
    {
        fillDrinksList();
        fillIngList();
        fillCartList();
    }

    /**
     * Initialise les touch listeners, pour effectuer des actions avant le relâchement du toucher
     */
    void setTouchListeners()
    {
        final ImageButton trashBTN=findViewById(R.id.trash_IMGBTN);
        final TextView noteExitBTN=findViewById(R.id.exitNoteBTN);

        drinkBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                    drinkBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.grey)));
                    drinkBTN.setBackgroundResource(R.drawable.icondrink);
                return false;
            }
        });

        ingBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                ingBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.grey)));
                ingBTN.setBackgroundResource(R.drawable.iconmix);
                return false;
            }
        });

        cartBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                cartBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.grey)));
                cartBTN.setBackgroundResource(R.drawable.iconcart);
                return false;
            }
        });

        optionsBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                optionsBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.grey)));
                optionsBTN.setBackgroundResource(R.drawable.iconoptions);
                return false;
            }
        });

        trashBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                trashBTN.setBackgroundColor(getResources().getColor(R.color.grey));
                return false;
            }
        });

        noteExitBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                noteExitBTN.setBackgroundColor(getResources().getColor(R.color.darkgrey));
                return false;
            }
        });
    }

    /**
     * Initialise les click listeners, pour effectuer des actions au relâchement du toucher
     */
    void setClickListeners()
    {
        final ImageButton trashBTN=findViewById(R.id.trash_IMGBTN);
        final TextView trashAllBTN=findViewById(R.id.trashall_BTN);
        final TextView noteExitBTN=findViewById(R.id.exitNoteBTN);
        final TextView triNoteBTN=findViewById(R.id.triNote_BTN);

        drinkBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                drinkBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.white)));
                drinkBTN.setBackgroundResource(R.drawable.icondrink);

                if(listDrinkLYT.getVisibility()!=View.VISIBLE) {
                    listDrinkLYT.setVisibility(View.VISIBLE);
                    infosLYT.setVisibility(View.INVISIBLE);
                }
                else
                {
                    listDrinkLYT.setVisibility(View.INVISIBLE);
                    infosLYT.setVisibility(View.VISIBLE);
                }
                listIngLYT.setVisibility(View.INVISIBLE);
                optionsLYT.setVisibility(View.INVISIBLE);
                cartLYT.setVisibility(View.INVISIBLE);

                fillDrinksList();
            }
        });

        ingBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                ingBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.white)));
                ingBTN.setBackgroundResource(R.drawable.iconmix);

                if(listIngLYT.getVisibility()!=View.VISIBLE) {
                    listIngLYT.setVisibility(View.VISIBLE);
                    infosLYT.setVisibility(View.INVISIBLE);
                }
                else
                {
                    listIngLYT.setVisibility(View.INVISIBLE);
                    infosLYT.setVisibility(View.VISIBLE);
                }
                listDrinkLYT.setVisibility(View.INVISIBLE);
                optionsLYT.setVisibility(View.INVISIBLE);
                cartLYT.setVisibility(View.INVISIBLE);

                fillIngList();
            }
        });

        cartBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                cartBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.white)));
                cartBTN.setBackgroundResource(R.drawable.iconcart);

                if(cartLYT.getVisibility()!=View.VISIBLE) {
                    cartLYT.setVisibility(View.VISIBLE);
                    selectFirstCartItem();
                }
                else
                {
                    cartLYT.setVisibility(View.INVISIBLE);
                    infosLYT.setVisibility(View.VISIBLE);
                }
                infosLYT.setVisibility(View.INVISIBLE);
                listDrinkLYT.setVisibility(View.INVISIBLE);
                listIngLYT.setVisibility(View.INVISIBLE);
                optionsLYT.setVisibility(View.INVISIBLE);
            }
        });

        optionsBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                optionsBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.white)));
                optionsBTN.setBackgroundResource(R.drawable.iconoptions);

                if(optionsLYT.getVisibility()!=View.VISIBLE) {
                    optionsLYT.setVisibility(View.VISIBLE);
                    infosLYT.setVisibility(View.INVISIBLE);

                }
                else
                {
                    optionsLYT.setVisibility(View.INVISIBLE);
                    infosLYT.setVisibility(View.VISIBLE);
                }
                listDrinkLYT.setVisibility(View.INVISIBLE);
                listIngLYT.setVisibility(View.INVISIBLE);
                cartLYT.setVisibility(View.INVISIBLE);
            }
        });

        trashBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                trashBTN.setBackgroundColor(getResources().getColor(R.color.white));
                RetirerPanier();
                trashBTN.setVisibility(View.INVISIBLE);
            }
        });

        trashAllBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                selectedCartPositions.clear();
                arrayListCart.clear();
                fillCartList();
                trashBTN.setVisibility(View.INVISIBLE);
            }
        });

        noteExitBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                noteExitBTN.setBackgroundColor(getResources().getColor(R.color.grey));
                AnnulerNote();
            }
        });

        triNoteBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if(triNoteBTN.getText().equals("▼"))
                {
                    triNoteBTN.setText("▲");
                    TrierEtoileHaut();
                }
                else if(triNoteBTN.getText().equals("▲"))
                {
                    triNoteBTN.setText("~");
                    EnleverTri();
                }
                else if(triNoteBTN.getText().equals("~"))
                {
                    triNoteBTN.setText("▼");
                    TrierEtoileBas();
                }
            }
        });


        listDrinkLVIEW.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int position,
                                    long id) {

                HashMap<String, String> item = ( HashMap<String, String>)adapterView.getItemAtPosition(position);
                Toast toast = Toast.makeText(getApplicationContext(),
                        "x1 " + item.values().toArray()[1] + " ajouté au panier", Toast.LENGTH_SHORT);
                toast.setGravity(Gravity.CENTER, 0, 500);
                toast.show();

                AjouterPanier(item);
            }
        });

        listIngLVIEW.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int position,
                                    long id) {

                HashMap<String, String> item = ( HashMap<String, String>)adapterView.getItemAtPosition(position);
                Toast toast = Toast.makeText(getApplicationContext(),
                        "x1 " + item.values().toArray()[0] + " ajouté au panier", Toast.LENGTH_SHORT);
                toast.setGravity(Gravity.CENTER, 0, 500);
                toast.show();

                AjouterPanier(item);
            }
        });

        cartLVIEW.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int position,
                                    long id) {
                if(view != null && view.getContext() != null) {
                    trashBTN.setVisibility(View.VISIBLE);

                    if (arrayListCart.size() < position)
                        selectedCartPositions.add(0);
                    if (selectedCartPositions.contains(position))
                        selectedCartPositions.remove(selectedCartPositions.indexOf(position));
                    else if (!selectedCartPositions.contains(position))
                        selectedCartPositions.add(position);

                    if (selectedCartPositions.size() > 0) {
                        if (arrayListCart.size() < position)
                            cartLVIEW.getChildAt(0).setBackgroundColor(getResources().getColor(R.color.yellow));
                        if (selectedCartPositions.contains(position))
                            cartLVIEW.getChildAt(position).setBackgroundColor(getResources().getColor(R.color.yellow));
                        else
                            cartLVIEW.getChildAt(position).setBackgroundColor(getResources().getColor(R.color.white));
                    }
                }
            }
        });
    }

    /**
     * Ajoute l'élément envoyé en paramètre dans le panier
     */
    void AjouterPanier(HashMap<String, String> ajout)
    {
        arrayListCart.add(ajout);
        fillCartList();
    }

    /**
     * Supprime l'élément envoyé en paramètre du panier
     */
    @Deprecated
    void RetirerPanier(HashMap<String, String> retrait)
    {
        if(retrait!=null) {
            arrayListCart.remove(retrait);
            fillCartList();
            Toast toast = Toast.makeText(getApplicationContext(),
                    "x1 " + retrait.values().toArray()[0] + " retiré du panier", Toast.LENGTH_SHORT);
            toast.setGravity(Gravity.CENTER, 0, 350);
            toast.show();
        }
    }

    /**
     * Supprime l'élément courant selectionné du panier
     */
    void RetirerPanier()
    {
        int compteurItems=0;
        for (; compteurItems<selectedCartPositions.size();compteurItems++)
        {
            arrayListCart.set(selectedCartPositions.get(compteurItems),null);
        }
        while(arrayListCart.remove(null));
        selectedCartPositions.clear();
        fillCartList();
        Toast toast = Toast.makeText(getApplicationContext(),
                "x " + compteurItems + " items retiré du panier", Toast.LENGTH_SHORT);
        toast.setGravity(Gravity.CENTER, 0, 350);
        toast.show();
    }

    /**
     * Vide puis rempli la liste des drinks disponibles à partir de la BD(Pour l'initialiser, puis la rafraîchir)
     */
    void fillDrinksList()
    {
        arrayListDrink.clear();
        for (int i=0;i<DrinkName.length;i++)
        {
            HashMap<String,String> hashMap=new HashMap<>();//create a hashmap to store the data in key value pair
            hashMap.put("nom",DrinkName[i]);
            hashMap.put("desc",DrinksIngredients[i]+"");
            hashMap.put("note",Notes[i]);
            arrayListDrink.add(hashMap);//add the hashmap into arrayList
        }
        EnleverTri();
        refreshDrinkList();
    }

    void refreshDrinkList()
    {
        SimpleAdapter simpleAdapter=new SimpleAdapter(this, arrayListDrink,R.layout.custom_list_drink,from,to);
        listDrinkLVIEW.setAdapter(simpleAdapter);//sets the adapter for listView
    }

    /**
     * Vide puis rempli la liste des ingrédients disponibles à partir de la BD(Pour l'initialiser, puis la rafraîchir)
     */
    void fillIngList()
    {
        arrayListIng.clear();
        for (int i=0;i<IngName.length;i++)
        {
            HashMap<String,String> hashMap=new HashMap<>();//create a hashmap to store the data in key value pair
            hashMap.put("nom",IngName[i]);
            //hashMap.put("desc",Ingredients[i]+"");
            //hashMap.put("note",Notes[i]);
            arrayListIng.add(hashMap);//add the hashmap into arrayList
        }
        Collections.sort(arrayListIng, new Comparator<HashMap<String,String>>()
        {
            public int compare(HashMap<String,String> o1,
                               HashMap<String,String> o2)
            {
                return o1.get("nom").compareTo(o2.get("nom"));
            }
        });
        refreshIngList();
    }

    void refreshIngList()
    {
        SimpleAdapter simpleAdapter=new SimpleAdapter(this,arrayListIng,R.layout.custom_list_ing,from,to);
        listIngLVIEW.setAdapter(simpleAdapter);//sets the adapter for listView
    }

    /**
     * Permet d'initialiser et rafraîchir la liste du panier
     */
    void fillCartList()
    {
        SimpleAdapter simpleAdapter=new SimpleAdapter(this,arrayListCart,R.layout.custom_list_ing,from,to);
        cartLVIEW.setAdapter(simpleAdapter);//sets the adapter for listView
    }

    @Deprecated
    void selectFirstCartItem()
    {
        if(arrayListCart.size()>0)
        {
            int defaultPosition = 0;
            int justIgnoreId = 0;
            cartLVIEW.setItemChecked(defaultPosition, true);
            cartLVIEW.performItemClick(cartLVIEW.getSelectedView(),defaultPosition, justIgnoreId);
        }
    }

    void Commander() {

    }

    void AnnulerNote() {
        notesLYT.setVisibility(View.INVISIBLE);
    }

    void EnvoyerNote() {

        Toast toast = Toast.makeText(getApplicationContext(),
                "Merci d'avoir noté!", Toast.LENGTH_SHORT);
        toast.setGravity(Gravity.CENTER, 0, 500);
        toast.show();
        notesLYT.setVisibility(View.INVISIBLE);
    }

    void TrierEtoileHaut()
    {
        Collections.sort(arrayListDrink, new Comparator<HashMap<String,String>>()
        {
            public int compare(HashMap<String,String> o1,
                               HashMap<String,String> o2)
            {
                float o1note=Float.valueOf(o1.get("note"));
                float o2note=Float.valueOf(o2.get("note"));
                if (o1note < o2note)
                {
                    return -1;
                }
                else if (o1note > o2note)
                {
                    return 1;
                }
                return 0;
            }
        });

        refreshDrinkList();
    }

    void TrierEtoileBas()
    {
        Collections.sort(arrayListDrink, new Comparator<HashMap<String,String>>()
        {
            public int compare(HashMap<String,String> o1,
                               HashMap<String,String> o2)
            {
                float o1note=Float.valueOf(o1.get("note"));
                float o2note=Float.valueOf(o2.get("note"));
                if (o1note < o2note)
                {
                    return 1;
                }
                else if (o1note > o2note)
                {
                    return -1;
                }
                return 0;
            }
        });
        refreshDrinkList();
    }

    void EnleverTri()
    {
        Collections.sort(arrayListDrink, new Comparator<HashMap<String,String>>()
        {
            public int compare(HashMap<String,String> o1,
                               HashMap<String,String> o2)
            {
                return o1.get("nom").compareTo(o2.get("nom"));
            }
        });
        refreshDrinkList();
    }
}