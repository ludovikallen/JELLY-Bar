package com.example.a201625221.projetjelly;

import android.content.res.ColorStateList;
import android.graphics.ColorMatrixColorFilter;
import android.graphics.Paint;
import android.graphics.drawable.Drawable;
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
import java.util.HashMap;

public class MainActivity extends AppCompatActivity {


    /**
     * Variables pour contenir les layouts pour pouvoir changer d'onglet dans l'application
     */
    ConstraintLayout listLYT,optionsLYT,cartLYT,infosLYT;

    /**
     * Variables pour contenir les boutons pour pouvoir changer d'onglet dans l'application
     */
    Button drinkBTN,ingBTN,cartBTN,optionsBTN;

    /**
     * Variables permettant d'afficher dans la ListView les éléments des ArrayList<HashMap<String,String>> en passant par l'adapter
     */
    ListView listLVIEW,cartLVIEW;

    /**
     * Tableaux pour indiquer l'origine et la destination graphique
     */
    String from[]={"nom","ing"};
    /**
     * Tableaux pour indiquer l'origine et la destination graphique
     */
    int to[]={R.id.name_TXT,R.id.ing_TXT};

    /**
     * Listes contenant les éléments de la BD et le panier
     */
    ArrayList<HashMap<String,String>> arrayListNoms=new ArrayList<>(),arrayListIng=new ArrayList<>(),arrayListCart=new ArrayList<>();

    /**
     * Variable contenant l'objet selectionné dans le panier
     */
    HashMap<String,String> SelectedCartItem;

    String[] DrinkName={"Sex on the beach","Cosmopolitan","Rhum and coke","Beer","Diesel","Water"};
    String[] Ingredients={"Vodka+OrangeJuice+Grenadine","xxx","Rhum+Coke","Beer","Beer+Coke","Water"};

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
        listLYT=findViewById(R.id.list_LYT);
        optionsLYT=findViewById(R.id.options_LYT);
        cartLYT=findViewById(R.id.cart_LYT);
        infosLYT=findViewById(R.id.infos_LYT);

        drinkBTN=findViewById(R.id.drinklist_BTN);
        ingBTN=findViewById(R.id.inglist_BTN);
        cartBTN=findViewById(R.id.cart_BTN);
        optionsBTN=findViewById(R.id.options_BTN);

        listLVIEW=findViewById(R.id.drinking_LVIEW);
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
    }

    /**
     * Initialise les click listeners, pour effectuer des actions au relâchement du toucher
     */
    void setClickListeners()
    {
        final ImageButton trashBTN=findViewById(R.id.trash_IMGBTN);

        drinkBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                TextView label=findViewById(R.id.labeldrinking_TXT);
                drinkBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.white)));
                drinkBTN.setBackgroundResource(R.drawable.icondrink);

                if(listLYT.getVisibility()!=View.VISIBLE) {
                    listLYT.setVisibility(View.VISIBLE);
                    infosLYT.setVisibility(View.INVISIBLE);
                }
                else if(label.getText().equals(getString(R.string.drinks_str)))
                {
                    listLYT.setVisibility(View.INVISIBLE);
                    infosLYT.setVisibility(View.VISIBLE);
                }
                optionsLYT.setVisibility(View.INVISIBLE);
                cartLYT.setVisibility(View.INVISIBLE);

                label.setText(getString(R.string.drinks_str));
                label.setPaintFlags(label.getPaintFlags()| Paint.UNDERLINE_TEXT_FLAG);

                fillDrinksList();
            }
        });

        ingBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                TextView label=findViewById(R.id.labeldrinking_TXT);
                ingBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.white)));
                ingBTN.setBackgroundResource(R.drawable.iconmix);

                if(listLYT.getVisibility()!=View.VISIBLE) {
                    listLYT.setVisibility(View.VISIBLE);
                    infosLYT.setVisibility(View.INVISIBLE);
                }
                else if(label.getText().equals(getString(R.string.ingredients_str)))
                {
                    listLYT.setVisibility(View.INVISIBLE);
                    infosLYT.setVisibility(View.VISIBLE);
                }
                optionsLYT.setVisibility(View.INVISIBLE);
                cartLYT.setVisibility(View.INVISIBLE);

                label.setText(getString(R.string.ingredients_str));
                label.setPaintFlags(label.getPaintFlags()| Paint.UNDERLINE_TEXT_FLAG);

                fillIngList();
            }
        });

        cartBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                cartBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.white)));
                cartBTN.setBackgroundResource(R.drawable.iconcart);

                if(cartLYT.getVisibility()!=View.VISIBLE) {
                    cartLYT.setVisibility(View.VISIBLE);
                }
                else
                {
                    cartLYT.setVisibility(View.INVISIBLE);
                }
                infosLYT.setVisibility(View.INVISIBLE);
                listLYT.setVisibility(View.INVISIBLE);
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
                listLYT.setVisibility(View.INVISIBLE);
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

        listLVIEW.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int position,
                                    long id) {

                HashMap<String, String> item = ( HashMap<String, String>)adapterView.getItemAtPosition(position);
                Toast toast = Toast.makeText(getApplicationContext(),
                        "x1 " + item.values().toArray()[0] + " ajouté au panier", Toast.LENGTH_SHORT);
                toast.setGravity(Gravity.CENTER, 0, 350);
                toast.show();

                AjouterPanier(item);
            }
        });

        cartLVIEW.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int position,
                                    long id) {

                SelectedCartItem = ( HashMap<String, String>)adapterView.getItemAtPosition(position);
                trashBTN.setVisibility(View.VISIBLE);
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
        if(SelectedCartItem !=null) {
            arrayListCart.remove(SelectedCartItem);
            fillCartList();
            Toast toast = Toast.makeText(getApplicationContext(),
                    "x1 " + SelectedCartItem.values().toArray()[0] + " retiré du panier", Toast.LENGTH_SHORT);
            toast.setGravity(Gravity.CENTER, 0, 350);
            toast.show();
            SelectedCartItem = null;
        }
    }

    /**
     * Vide puis rempli la liste des drinks disponibles à partir de la BD(Pour l'initialiser, puis la rafraîchir)
     */
    void fillDrinksList()
    {
        arrayListNoms.clear();
        for (int i=0;i<DrinkName.length;i++)
        {
            HashMap<String,String> hashMap=new HashMap<>();//create a hashmap to store the data in key value pair
            hashMap.put("nom",DrinkName[i]);
            hashMap.put("ing",Ingredients[i]+"");
            arrayListNoms.add(hashMap);//add the hashmap into arrayList
        }
        SimpleAdapter simpleAdapter=new SimpleAdapter(this,arrayListNoms,R.layout.custom_list,from,to);
        listLVIEW.setAdapter(simpleAdapter);//sets the adapter for listView
    }

    /**
     * Vide puis rempli la liste des ingrédients disponibles à partir de la BD(Pour l'initialiser, puis la rafraîchir)
     */
    void fillIngList()
    {
        arrayListIng.clear();
        for (int i=0;i<DrinkName.length;i++)
        {
            HashMap<String,String> hashMap=new HashMap<>();//create a hashmap to store the data in key value pair
            hashMap.put("nom",DrinkName[i]);
            hashMap.put("ing",Ingredients[i]+"");
            arrayListIng.add(hashMap);//add the hashmap into arrayList
        }
        SimpleAdapter simpleAdapter=new SimpleAdapter(this,arrayListIng,R.layout.custom_list,from,to);
        listLVIEW.setAdapter(simpleAdapter);//sets the adapter for listView
    }

    /**
     * Permet d'initialiser et rafraîchir la liste du panier
     */
    void fillCartList()
    {
        SimpleAdapter simpleAdapter=new SimpleAdapter(this,arrayListCart,R.layout.custom_list,from,to);
        cartLVIEW.setAdapter(simpleAdapter);//sets the adapter for listView
    }

    void Commander() {
        
    }
}