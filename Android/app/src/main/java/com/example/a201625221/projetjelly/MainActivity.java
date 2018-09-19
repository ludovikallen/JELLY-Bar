package com.example.a201625221.projetjelly;

import android.content.res.ColorStateList;
import android.graphics.Color;
import android.graphics.Paint;
import android.support.constraint.ConstraintLayout;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Gravity;
import android.view.MotionEvent;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class MainActivity extends AppCompatActivity {

    //Variables pour contenir les layouts pour pouvoir changer d'onglet dans l'application
    ConstraintLayout listLYT;
    ConstraintLayout optionsLYT;
    ConstraintLayout cartLYT;
    ConstraintLayout infosLYT;

    //Variables pour contenir les boutons pour pouvoir changer d'onglet dans l'application
    Button drinkBTN;
    Button ingBTN;
    Button cartBTN;
    Button optionsBTN;

    //Variables permettant d'afficher dans la ListView les éléments de l'ArrayList en passant par l'adapter
    ListView listLVIEW;
    ListView cartLVIEW;

    String from[]={"nom","ing"};
    int to[]={R.id.name_TXT,R.id.ing_TXT};

    ArrayList<HashMap<String,String>> arrayListNoms;
    ArrayList<HashMap<String,String>> arrayListIng;
    ArrayList<HashMap<String,String>> arrayListCart;

    HashMap<String,String> SelectedItem;

    String[] animalName1={"Lion","Tiger","Monkey","Dog","Cat","Elephant"};//animal names array
    String[] animalName2={"Lion1","Tiger2","Monkey3","Dog4","Cat5","Elephant6"};//animal names array
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        InitializeComponents();
        InitLists();
        setTouchListeners();
        setClickListeners();
    }

    //Initialise les composantes globales utilisées plusieurs fois pour ne pas avoir à les rechercher dans les fonctions
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

    //Initialise les touch listeners, pour effectuer des actions avant le relâchement du toucher
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

    //Initialise les click listeners, pour effectuer des actions au relâchement du toucher
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
                    infosLYT.setVisibility(View.INVISIBLE);
                }
                else
                {
                    cartLYT.setVisibility(View.INVISIBLE);
                    infosLYT.setVisibility(View.VISIBLE);
                }
                listLYT.setVisibility(View.INVISIBLE);
                optionsLYT.setVisibility(View.INVISIBLE);

                fillCartList();
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
                RetirerPanier(SelectedItem);
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

                SelectedItem = ( HashMap<String, String>)adapterView.getItemAtPosition(position);
            }
        });
    }

    void AjouterPanier(HashMap<String, String> ajout)
    {
        arrayListCart.add(ajout);
        fillCartList();
    }

    void RetirerPanier(HashMap<String, String> retrait)
    {
        arrayListCart.remove(retrait);
        fillCartList();
        Toast toast = Toast.makeText(getApplicationContext(),
                "x1 " + SelectedItem.values().toArray()[0] + " retiré du panier", Toast.LENGTH_SHORT);
        toast.setGravity(Gravity.CENTER, 0, 350);
        toast.show();
        SelectedItem=null;
    }

    void fillDrinksList()
    {
        arrayListNoms.clear();
        for (int i=0;i<animalName1.length;i++)
        {
            HashMap<String,String> hashMap=new HashMap<>();//create a hashmap to store the data in key value pair
            hashMap.put("nom",animalName1[i]);
            hashMap.put("ing",animalName2[i]+"");
            arrayListNoms.add(hashMap);//add the hashmap into arrayList
        }
        SimpleAdapter simpleAdapter=new SimpleAdapter(this,arrayListNoms,R.layout.custom_list,from,to);
        listLVIEW.setAdapter(simpleAdapter);//sets the adapter for listView
    }

    void fillIngList()
    {
        arrayListIng.clear();
        for (int i=0;i<animalName1.length;i++)
        {
            HashMap<String,String> hashMap=new HashMap<>();//create a hashmap to store the data in key value pair
            hashMap.put("nom",animalName1[i]);
            hashMap.put("ing",animalName2[i]+"");
            arrayListIng.add(hashMap);//add the hashmap into arrayList
        }
        SimpleAdapter simpleAdapter=new SimpleAdapter(this,arrayListIng,R.layout.custom_list,from,to);
        listLVIEW.setAdapter(simpleAdapter);//sets the adapter for listView
    }

    void fillCartList()
    {
        SimpleAdapter simpleAdapter=new SimpleAdapter(this,arrayListCart,R.layout.custom_list,from,to);
        cartLVIEW.setAdapter(simpleAdapter);//sets the adapter for listView
    }

    void InitLists()
    {
        arrayListNoms=new ArrayList<>();
        arrayListIng=new ArrayList<>();
        arrayListCart=new ArrayList<>();
        fillDrinksList();
        fillIngList();
        fillCartList();
    }

}

