package com.example.a201625221.projetjelly;

import android.annotation.SuppressLint;
import android.content.res.ColorStateList;
import android.content.res.Resources;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.PorterDuff;
import android.graphics.Rect;
import android.support.constraint.ConstraintLayout;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Gravity;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.ListView;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;
import android.os.StrictMode;
import org.w3c.dom.Text;

import java.math.RoundingMode;
import java.sql.PreparedStatement;
import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.Iterator;
import java.util.Set;

import static android.media.CamcorderProfile.get;

public class MainActivity extends AppCompatActivity {
    public static Connection conn_ = null;
    int toast_height=420;
    /**
     * Variables pour contenir les layouts pour pouvoir changer d'onglet dans l'application
     */
    ConstraintLayout listDrinkLYT, modifyLYT,optionsLYT,cartLYT,infosLYT,notesLYT, connexionLYT;

    RadioGroup radioGRP;
    /**
     * Variables pour contenir les boutons pour pouvoir changer d'onglet dans l'application
     */
    Button drinkBTN,cartBTN,optionsBTN,infoBTN;

    /**
     * Variables permettant d'afficher dans la ListView les éléments des ArrayList<HashMap<String,String>> en passant par l'adapter
     */
    ListView listDrinkLVIEW,listIngLVIEW,cartLVIEW, drinkItemLVIEW;

    /**
     * Tableaux pour indiquer l'origine des données de l'adapter
     */
    String from[]={"nom","desc","note"};
    /**
     * Tableau la destination graphique de l'adapter
     */
    int toDrink[]={R.id.nameDrink_TXT,R.id.descDrink_TXT,R.id.noteDrink_TXT};
    int toIng[]={R.id.nameIng_TXT,R.id.descIng_TXT};
    int toCourant[]={R.id.nameCourant_TXT,R.id.descCourant_TXT,R.id.noteCourant_TXT};
    /**
     * Listes contenant les éléments de la BD et le panier
     */
    ArrayList<HashMap<String,String>> arrayListDrink =new ArrayList<>(),arrayListIng=new ArrayList<>(),arrayListCart=new ArrayList<>(), arrayListItemCourant=new ArrayList<>();

    ArrayList<Integer>selectedCartPositions=new ArrayList<>();
    /**
     * Variable contenant l'objet selectionné dans le panier
     */

   HashMap<String, ColorStateList> couleurs=new HashMap<>();

    int indexItemModification=-1;
    String couleurCourante;
    Integer note=0;
    /**
     * Fonction lancée à la création de l'activité
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        StrictMode.setThreadPolicy(new
                StrictMode.ThreadPolicy.Builder()
                .detectDiskReads()
                .detectDiskWrites()
                .detectNetwork()
                .penaltyLog()
                .build());
        StrictMode.setVmPolicy(new StrictMode.VmPolicy.Builder()
                .detectLeakedSqlLiteObjects()
                .penaltyLog()
                .penaltyDeath()
                .build());
        OracleConnexion();

    }

    private void OracleConnexion(){
        Thread t= new Thread() {
            @Override
            public void run() {
                try
                {
                    Class.forName("oracle.jdbc.OracleDriver");
                }
                catch (ClassNotFoundException e)
                {
                    Toast.makeText(MainActivity.this, "Driver manquant." +
                            e.getMessage().toString(), Toast.LENGTH_LONG).show();
                }
                String jdbcURL = "jdbc:oracle:thin:@mercure.clg.qc.ca:1521:ORCL";
                String user = "barman";
                String passwd = "projet";
                try
                {
                    conn_ = DriverManager.getConnection(jdbcURL,user,passwd);
                    Initialize();

                }
                catch (java.sql.SQLException se)
                {
                    faireToast("Connexion au serveur  impossible." + se.getMessage());
                }
            }
        };
        t.start();
    }

    /**
     * Appel de toutes les fonctions d'initialisation
     */
    void Initialize()
    {
        InitializeComponents();
        InitLists();
        InitColors();
        setTouchListeners();
        setClickListeners();
        setCheckedListeners();
    }

    /**
     * Initialise les composantes globales utilisées plusieurs fois pour ne pas avoir à les rechercher dans les fonctions
     */
     void InitializeComponents()
    {
        listDrinkLYT=findViewById(R.id.listDrink_LYT);
        modifyLYT =findViewById(R.id.listIng_LYT);
        optionsLYT=findViewById(R.id.options_LYT);
        cartLYT=findViewById(R.id.cart_LYT);
        infosLYT=findViewById(R.id.infos_LYT);
        notesLYT=findViewById(R.id.notes_LYT);
        connexionLYT=findViewById(R.id.connexion_LYT);

        drinkBTN=findViewById(R.id.drinklist_BTN);
        infoBTN=findViewById(R.id.infos_BTN);
        cartBTN=findViewById(R.id.cart_BTN);
        optionsBTN=findViewById(R.id.options_BTN);

        listDrinkLVIEW=findViewById(R.id.drink_LVIEW);
        listIngLVIEW=findViewById(R.id.ing_LVIEW);
        cartLVIEW=findViewById(R.id.cart_LVIEW);
        drinkItemLVIEW=findViewById(R.id.drinkItem_LVIEW);

        radioGRP=findViewById(R.id.changerCouleur_RBTNGRP);
    }

    /**
     * Initialise les trois listes principales, puis les remplis à partir de la BD
     */
    void InitLists()
    {
        fillDrinksList();
        fillIngList();
        fillCartList();
       // refreshCartItemCount();
    }

    void InitColors()
    {
        int[][] states = new int[][] {
                new int[] { }
        };
        int[] colors = new int[] {
                getResources().getColor(R.color.yellow),
        };

        ColorStateList jaune = new ColorStateList(states, colors);
        couleurs.put("jaune",jaune);

        states = new int[][] {
                new int[] { }
        };
        colors = new int[] {
                getResources().getColor(R.color.black),
        };

        ColorStateList noir = new ColorStateList(states, colors);
        couleurs.put("noir",noir);

        states = new int[][] {
                new int[] { }
        };
        colors = new int[] {
                getResources().getColor(R.color.white),
        };

        ColorStateList blanc = new ColorStateList(states, colors);
        couleurs.put("blanc",blanc);
    }

    /**
     * Initialise les touch listeners, pour effectuer des actions avant le relâchement du toucher
     */
    @SuppressLint("ClickableViewAccessibility")
    void setTouchListeners()
    {
        final ImageButton trashBTN=findViewById(R.id.trash_IMGBTN);
        final ImageButton commandBTN=findViewById(R.id.command_IMGBTN);
        final TextView noteExitBTN=findViewById(R.id.exitNoteBTN);

        final ImageButton etoile1= findViewById(R.id.star1_IMGBTN);
        final ImageButton etoile2= findViewById(R.id.star2_IMGBTN);
        final ImageButton etoile3= findViewById(R.id.star3_IMGBTN);
        final ImageButton etoile4= findViewById(R.id.star4_IMGBTN);
        final ImageButton etoile5= findViewById(R.id.star5_IMGBTN);

        drinkBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                    drinkBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.grey)));
                    drinkBTN.setBackgroundResource(R.drawable.icondrink);
                return false;
            }
        });

        infoBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                infoBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.grey)));
                infoBTN.setBackgroundResource(R.drawable.iconinfo);
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
        commandBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                commandBTN.setBackgroundColor(getResources().getColor(R.color.grey));
                return false;
            }
        });

        etoile1.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                etoile1.setBackgroundTintMode(PorterDuff.Mode.CLEAR);
                return false;
            }
        });
        etoile2.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                etoile2.setBackgroundTintMode(PorterDuff.Mode.CLEAR);
                return false;
            }
        });
        etoile3.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                etoile3.setBackgroundTintMode(PorterDuff.Mode.CLEAR);
                return false;
            }
        });
        etoile4.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                etoile4.setBackgroundTintMode(PorterDuff.Mode.CLEAR);
                return false;
            }
        });
        etoile5.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                etoile5.setBackgroundTintMode(PorterDuff.Mode.CLEAR);
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
        final ImageButton commandBTN=findViewById(R.id.command_IMGBTN);
        final TextView trashAllBTN=findViewById(R.id.trashall_BTN);
        final TextView noteExitBTN=findViewById(R.id.exitNoteBTN);
        final TextView noteSendBTN=findViewById(R.id.sendNote_BTN);
        final TextView triNoteBTN=findViewById(R.id.triNote_BTN);
        final Button acceptChangesBTN=findViewById(R.id.acceptChange_BTN);
        final Button cancelChangesBTN=findViewById(R.id.cancelChange_BTN);
        final Button connect= findViewById(R.id.connexion_BTN);

        final ImageButton etoile1= findViewById(R.id.star1_IMGBTN);
        final ImageButton etoile2= findViewById(R.id.star2_IMGBTN);
        final ImageButton etoile3= findViewById(R.id.star3_IMGBTN);
        final ImageButton etoile4= findViewById(R.id.star4_IMGBTN);
        final ImageButton etoile5= findViewById(R.id.star5_IMGBTN);

        drinkBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if(radioGRP.getCheckedRadioButtonId() ==R.id.changerBlanc_RBTN)
                    changeMenuButtonsColor(couleurs.get("blanc"));
                else if(radioGRP.getCheckedRadioButtonId() ==R.id.changerNoir_RBTN)
                    changeMenuButtonsColor(couleurs.get("noir"));
                else if(radioGRP.getCheckedRadioButtonId() ==R.id.changerJelly_RBTN)
                    changeMenuButtonsColor(couleurs.get("jaune"));
                drinkBTN.setBackgroundResource(R.drawable.icondrink);

                listDrinkLYT.setVisibility(View.VISIBLE);
                modifyLYT.setVisibility(View.INVISIBLE);
                optionsLYT.setVisibility(View.INVISIBLE);
                cartLYT.setVisibility(View.INVISIBLE);
                infosLYT.setVisibility(View.INVISIBLE);

                fillDrinksList();
                EnleverTri();
            }
        });

        cartBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if(radioGRP.getCheckedRadioButtonId() ==R.id.changerBlanc_RBTN)
                    changeMenuButtonsColor(couleurs.get("blanc"));
                else if(radioGRP.getCheckedRadioButtonId() ==R.id.changerNoir_RBTN)
                    changeMenuButtonsColor(couleurs.get("noir"));
                else if(radioGRP.getCheckedRadioButtonId() ==R.id.changerJelly_RBTN)
                    changeMenuButtonsColor(couleurs.get("jaune"));
                cartBTN.setBackgroundResource(R.drawable.iconcart);

                listDrinkLYT.setVisibility(View.INVISIBLE);
                modifyLYT.setVisibility(View.INVISIBLE);
                optionsLYT.setVisibility(View.INVISIBLE);
                cartLYT.setVisibility(View.VISIBLE);
                infosLYT.setVisibility(View.INVISIBLE);
            }
        });

        optionsBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if(radioGRP.getCheckedRadioButtonId() ==R.id.changerBlanc_RBTN)
                    changeMenuButtonsColor(couleurs.get("blanc"));
                else if(radioGRP.getCheckedRadioButtonId() ==R.id.changerNoir_RBTN)
                    changeMenuButtonsColor(couleurs.get("noir"));
                else if(radioGRP.getCheckedRadioButtonId() ==R.id.changerJelly_RBTN)
                    changeMenuButtonsColor(couleurs.get("jaune"));
                optionsBTN.setBackgroundResource(R.drawable.iconoptions);

                listDrinkLYT.setVisibility(View.INVISIBLE);
                modifyLYT.setVisibility(View.INVISIBLE);
                optionsLYT.setVisibility(View.VISIBLE);
                cartLYT.setVisibility(View.INVISIBLE);
                infosLYT.setVisibility(View.INVISIBLE);
            }
        });

        infoBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if(radioGRP.getCheckedRadioButtonId() ==R.id.changerBlanc_RBTN)
                    changeMenuButtonsColor(couleurs.get("blanc"));
                else if(radioGRP.getCheckedRadioButtonId() ==R.id.changerNoir_RBTN)
                    changeMenuButtonsColor(couleurs.get("noir"));
                else if(radioGRP.getCheckedRadioButtonId() ==R.id.changerJelly_RBTN)
                    changeMenuButtonsColor(couleurs.get("jaune"));
                infoBTN.setBackgroundResource(R.drawable.iconinfo);

                listDrinkLYT.setVisibility(View.INVISIBLE);
                modifyLYT.setVisibility(View.INVISIBLE);
                optionsLYT.setVisibility(View.INVISIBLE);
                cartLYT.setVisibility(View.INVISIBLE);
                infosLYT.setVisibility(View.VISIBLE);
            }
        });

        trashBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {

                if(radioGRP.getCheckedRadioButtonId() ==R.id.changerBlanc_RBTN)
                    changeMenuButtonsColor(couleurs.get("blanc"));
                else if(radioGRP.getCheckedRadioButtonId() ==R.id.changerNoir_RBTN)
                    changeMenuButtonsColor(couleurs.get("noir"));
                else if(radioGRP.getCheckedRadioButtonId() ==R.id.changerJelly_RBTN)
                    changeMenuButtonsColor(couleurs.get("jaune"));
                RetirerPanier();
                trashBTN.setVisibility(View.INVISIBLE);
            }
        });

        commandBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                commandBTN.setBackgroundColor(getResources().getColor(R.color.white));
                Commander();
            }
        });

        trashAllBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                selectedCartPositions.clear();
                arrayListCart.clear();
                fillCartList();
                trashBTN.setVisibility(View.INVISIBLE);
                refreshCartItemCount();
            }
        });

        noteExitBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                noteExitBTN.setBackgroundColor(getResources().getColor(R.color.grey));
                AnnulerNote();
            }
        });

        noteSendBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                EnvoyerNote();
            }
        });

        triNoteBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if(triNoteBTN.getText().equals("▼"))
                {
                    TrierEtoileHaut();
                }
                else if(triNoteBTN.getText().equals("▲"))
                {
                    EnleverTri();
                }
                else if(triNoteBTN.getText().equals("A-B"))
                {
                    TrierEtoileBas();
                }
            }
        });

        acceptChangesBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if(indexItemModification!=-1) {
                    arrayListCart.remove(indexItemModification);
                }
                indexItemModification=-1;
                arrayListCart.add(arrayListItemCourant.get(0));
                arrayListItemCourant.clear();
                modifyLYT.setVisibility(View.INVISIBLE);
                cartLYT.setVisibility(View.VISIBLE);
                refreshCartItemCount();
                fillCartList();
            }
        });

        cancelChangesBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                indexItemModification=-1;
                arrayListItemCourant.clear();
                modifyLYT.setVisibility(View.INVISIBLE);
                listDrinkLYT.setVisibility(View.VISIBLE);
            }
        });

        connect.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                connexionLYT.setVisibility(View.INVISIBLE);
            }
        });

        etoile1.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                etoile1.setImageDrawable(getResources().getDrawable(R.drawable.star_active));
                etoile2.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
                etoile3.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
                etoile4.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
                etoile5.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
                note=1;
            }
        });

        etoile2.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                etoile1.setImageDrawable(getResources().getDrawable(R.drawable.star_active));
                etoile2.setImageDrawable(getResources().getDrawable(R.drawable.star_active));
                etoile3.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
                etoile4.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
                etoile5.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
                note=2;
            }
        });

        etoile3.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                etoile1.setImageDrawable(getResources().getDrawable(R.drawable.star_active));
                etoile2.setImageDrawable(getResources().getDrawable(R.drawable.star_active));
                etoile3.setImageDrawable(getResources().getDrawable(R.drawable.star_active));
                etoile4.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
                etoile5.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
                note=3;
            }
        });

        etoile4.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                etoile1.setImageDrawable(getResources().getDrawable(R.drawable.star_active));
                etoile2.setImageDrawable(getResources().getDrawable(R.drawable.star_active));
                etoile3.setImageDrawable(getResources().getDrawable(R.drawable.star_active));
                etoile4.setImageDrawable(getResources().getDrawable(R.drawable.star_active));
                etoile5.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
                note=4;
            }
        });

        etoile5.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                etoile1.setImageDrawable(getResources().getDrawable(R.drawable.star_active));
                etoile2.setImageDrawable(getResources().getDrawable(R.drawable.star_active));
                etoile3.setImageDrawable(getResources().getDrawable(R.drawable.star_active));
                etoile4.setImageDrawable(getResources().getDrawable(R.drawable.star_active));
                etoile5.setImageDrawable(getResources().getDrawable(R.drawable.star_active));
                note=5;
            }
        });


        listDrinkLVIEW.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int position,
                                    long id) {

                HashMap<String, String> item = ( HashMap<String, String>)adapterView.getItemAtPosition(position);
                faireToast("x1 " + item.values().toArray()[1] + " ajouté au panier");

                AjouterPanier(item);
            }
        });

        listIngLVIEW.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int position,
                                    long id) {

                HashMap<String, String> nouveauIngredient = ( HashMap<String, String>)adapterView.getItemAtPosition(position);
                faireToast("x1 " + nouveauIngredient.values().toArray()[0] + " ajouté au drink");

                HashMap<String, String> itemActuel=arrayListItemCourant.get(0);
                arrayListItemCourant.clear();
                HashMap<String, String> nouvelItemActuel=new HashMap<String, String>();

                HashMap<String, Integer> ingredients=défaireDescription(itemActuel.get("desc"));
                if(ingredients.containsKey(nouveauIngredient.get("nom")))
                {
                    int nbOz=ingredients.get(nouveauIngredient.get("nom"));
                    ingredients.remove(nouveauIngredient.get("nom"));
                    ingredients.put(nouveauIngredient.get("nom"),  nbOz + 1);
                }
                else
                    ingredients.put(nouveauIngredient.get("nom"), 1);
                String nouvelleDescription=faireDescription(ingredients);
                nouvelItemActuel.put("nom", itemActuel.get("nom"));
                nouvelItemActuel.put("desc", nouvelleDescription);
                nouvelItemActuel.put("note", itemActuel.get("note"));
                arrayListItemCourant.add(nouvelItemActuel);
                refreshItemCourant();
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
                            cartLVIEW.getChildAt(0).setBackgroundColor(getResources().getColor(R.color.listSelector));
                        if (selectedCartPositions.contains(position))
                            cartLVIEW.getChildAt(position).setBackgroundColor(getResources().getColor(R.color.listSelector));
                        else {
                            if(radioGRP.getCheckedRadioButtonId() ==R.id.changerBlanc_RBTN)
                                cartLVIEW.getChildAt(position).setBackgroundColor(couleurs.get("blanc").getDefaultColor());
                            else if(radioGRP.getCheckedRadioButtonId() ==R.id.changerNoir_RBTN)
                                cartLVIEW.getChildAt(position).setBackgroundColor(couleurs.get("noir").getDefaultColor());
                            else if(radioGRP.getCheckedRadioButtonId() ==R.id.changerJelly_RBTN)
                                cartLVIEW.getChildAt(position).setBackgroundColor(couleurs.get("jaune").getDefaultColor());
                        }
                    }
                }
            }
        });

        listDrinkLVIEW.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {

            public boolean onItemLongClick(AdapterView<?> adapterView, View view, int position, long id) {
                    arrayListItemCourant.clear();
                    HashMap<String, String> item = ( HashMap<String, String>)adapterView.getItemAtPosition(position);
                    arrayListItemCourant.add(item);
                    refreshItemCourant();

                    listDrinkLYT.setVisibility(View.INVISIBLE);
                    modifyLYT.setVisibility(View.VISIBLE);
                    fillIngList();
                    refreshIngList();
                return true;
            }
        });

        cartLVIEW.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {

            public boolean onItemLongClick(AdapterView<?> adapterView, View view, int position, long id) {
                    arrayListItemCourant.clear();
                    HashMap<String, String> item = ( HashMap<String, String>)adapterView.getItemAtPosition(position);
                    arrayListItemCourant.add(item);
                    refreshItemCourant();

                    cartLYT.setVisibility(View.INVISIBLE);
                    modifyLYT.setVisibility(View.VISIBLE);
                    fillIngList();
                    refreshIngList();
                    indexItemModification=position;
                return true;
            }
        });

        listIngLVIEW.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {

            public boolean onItemLongClick(AdapterView<?> adapterView, View view, int position,
                                    long id) {

                HashMap<String, String> nouveauIngredient = ( HashMap<String, String>)adapterView.getItemAtPosition(position);

                HashMap<String, String> itemActuel=arrayListItemCourant.get(0);
                HashMap<String, Integer> ingredients=défaireDescription(itemActuel.get("desc"));
                if(ingredients.containsKey(nouveauIngredient.get("nom")))
                {
                    arrayListItemCourant.clear();
                    HashMap<String, String> nouvelItemActuel = new HashMap<String, String>();


                    int nbOz=ingredients.get(nouveauIngredient.get("nom"));
                    ingredients.remove(nouveauIngredient.get("nom"));
                    if(nbOz-1!=0)
                        ingredients.put(nouveauIngredient.get("nom"),  nbOz - 1);

                    nouvelItemActuel.put("nom", itemActuel.get("nom"));
                    nouvelItemActuel.put("desc", faireDescription(ingredients));
                    nouvelItemActuel.put("note", itemActuel.get("note"));
                    faireToast("x1 " + nouveauIngredient.values().toArray()[0] + " enlevé du drink");
                    arrayListItemCourant.add(nouvelItemActuel);
                }
                else
                {
                    faireToast(nouveauIngredient.values().toArray()[0] + " pas dans le drink");
                }
                refreshItemCourant();
                return true;
            }
        });
    }

    public void setCheckedListeners()
    {
        final RadioGroup radioGroup = findViewById(R.id.changerCouleur_RBTNGRP);
        radioGroup.setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener()
        {
            @Override
            public void onCheckedChanged(RadioGroup group, int checkedId) {
                if(checkedId==R.id.changerBlanc_RBTN)
                {
                    changerBlanc();
                }
                else if(checkedId==R.id.changerNoir_RBTN)
                {
                    changerNoir();
                }
                else if(checkedId==R.id.changerJelly_RBTN)
                {
                    changerJELLY();
                }

                refreshItemCourant();
                fillDrinksList();
                fillIngList();
                fillCartList();
            }
        });
    }

    @Override
    public boolean dispatchTouchEvent(MotionEvent ev) {
        Rect viewRect = new Rect();
        notesLYT.getGlobalVisibleRect(viewRect);
        if(notesLYT.getVisibility()==View.VISIBLE) {
            if (!viewRect.contains((int) ev.getRawX(), (int) ev.getRawY())) {
                notesLYT.setVisibility(View.INVISIBLE);
                fillCartList();
                arrayListCart.clear();

                final ImageButton commandBTN=findViewById(R.id.command_IMGBTN);
                commandBTN.setVisibility(View.INVISIBLE);
                faireToast("Notes annulées");
                final TextView panierTXT=findViewById(R.id.cart_TXT);
                panierTXT.setText(getResources().getString(R.string.cart_str));
                panierTXT.setPaintFlags(panierTXT.getPaintFlags() |   Paint.UNDERLINE_TEXT_FLAG);
            }
        }

        return super.dispatchTouchEvent(ev);
    }

    /**
     * Ajoute l'élément envoyé en paramètre dans le panier
     */
    void AjouterPanier(HashMap<String, String> ajout)
    {
        arrayListCart.add(ajout);
        fillCartList();
        refreshCartItemCount();
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
            faireToast("x1 " + retrait.values().toArray()[0] + " retiré du panier");

        }
    }

    /**
     * Supprime les éléments courants selectionnés du panier
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
        if(compteurItems==1)
            faireToast(compteurItems + " item retiré du panier");
        else
            faireToast(compteurItems + " items retirés du panier");
        refreshCartItemCount();
    }

    /**
     * Vide puis rempli la liste des drinks disponibles à partir de la BD(Pour l'initialiser, puis la rafraîchir)
     */
    void fillDrinksList() {
        arrayListDrink.clear();
        Statement stm1;
        PreparedStatement stmlNote;
        ResultSet resultSet;
        ResultSet resultSetNote;
        int nombreRecette = compterNombreRecettes();

        for (int i = 1; i <= nombreRecette; i++)
        {
            try {
                String requeteDescription = "select recette.NOMRECETTE, NOMBOUTEILLE, INGREDIENTRECETTE.QTYSHOT,INGREDIENT.BOUTEILLEPRESENTE,INGREDIENT.QTYRESTANTE from INGREDIENT INNER JOIN INGREDIENTRECETTE ON INGREDIENT.CODEBOUTEILLE = INGREDIENTRECETTE.CODEBOUTEILLE INNER JOIN RECETTE ON INGREDIENTRECETTE.CODERECETTE = RECETTE.CODERECETTE WHERE RECETTE.CODERECETTE = " + i;
                stm1 = conn_.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE,ResultSet.CONCUR_READ_ONLY);

                resultSet = stm1.executeQuery(requeteDescription);
                String nom = null;
                String description = "";
                String Notetrouver = "";
                boolean drinkPossible = true;
                while(resultSet.next())
                {
                    String Note = "select AVG(SATISFACTION) from NOTE where Nomrecette =?";
                    nom = resultSet.getString(1);
                    stmlNote = conn_.prepareStatement(Note,ResultSet.TYPE_SCROLL_INSENSITIVE,ResultSet.CONCUR_READ_ONLY);
                    stmlNote.setString(1,nom);
                    resultSetNote = stmlNote.executeQuery();
                    resultSetNote.next();
                    description += resultSet.getString(3) +" oz " + resultSet.getString(2) +", ";
                    Notetrouver =  resultSetNote.getString(1);
                    if (resultSet.getString(4).equals("0") || resultSet.getInt(5) == 0)
                    {
                        drinkPossible = false;
                    }
                }
                if (!description.trim().equals("")){
                    description = description.substring(0, description.length() - 2);
                }
                HashMap<String,String> hashMap=new HashMap<>();//create a hashmap to store the data in key value pair
                hashMap.put("nom", nom);
                hashMap.put("desc",description);
                if(Notetrouver != null)
                {
                    hashMap.put("note",arrondir(Float.valueOf(Notetrouver)));
                }
                else{
                    hashMap.put("note","NA");
                }

                if (drinkPossible)
                {
                    arrayListDrink.add(hashMap);//add the hashmap into arrayList
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
        EnleverTri();
    }

    void refreshDrinkList()
    {
        SimpleAdapter simpleAdapter=new SimpleAdapter(this, arrayListDrink,R.layout.custom_list_drink,from,toDrink);
        SimpleAdapter.ViewBinder binder = new SimpleAdapter.ViewBinder() {
            @Override
            public boolean setViewValue(View view, Object object, String value) {

                if (view.equals((TextView) view.findViewById(R.id.nameDrink_TXT))) {
                    TextView nomTXT = (TextView) view.findViewById(R.id.nameDrink_TXT);
                    if(radioGRP.getCheckedRadioButtonId()==R.id.changerBlanc_RBTN) {
                        nomTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if(radioGRP.getCheckedRadioButtonId()==R.id.changerNoir_RBTN) {
                        nomTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if(radioGRP.getCheckedRadioButtonId()==R.id.changerJelly_RBTN) {
                        nomTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                if (view.equals((TextView) view.findViewById(R.id.descDrink_TXT))) {
                    TextView descTXT = (TextView) view.findViewById(R.id.descDrink_TXT);
                    if (radioGRP.getCheckedRadioButtonId() == R.id.changerBlanc_RBTN) {
                        descTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if (radioGRP.getCheckedRadioButtonId() == R.id.changerNoir_RBTN) {
                        descTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if (radioGRP.getCheckedRadioButtonId() == R.id.changerJelly_RBTN) {
                        descTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                if(view.equals((TextView) view.findViewById(R.id.noteDrink_TXT))) {
                    TextView noteTXT = (TextView) view.findViewById(R.id.noteDrink_TXT);
                    if (radioGRP.getCheckedRadioButtonId() == R.id.changerBlanc_RBTN) {
                        noteTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if (radioGRP.getCheckedRadioButtonId() == R.id.changerNoir_RBTN) {
                        noteTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if (radioGRP.getCheckedRadioButtonId() == R.id.changerJelly_RBTN) {
                        noteTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                return false;
            }
        };
        simpleAdapter.setViewBinder(binder);

        listDrinkLVIEW.setAdapter(simpleAdapter);//sets the adapter for listView
    }

    int compterNombreRecettes()
    {
        Statement stm1s;
        ResultSet setRecette;

        String requeteNombreRecette = "select count(*) from recette";
        try {
            stm1s = conn_.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE,ResultSet.CONCUR_READ_ONLY);
            setRecette = stm1s.executeQuery(requeteNombreRecette);
            setRecette.next();
            return setRecette.getInt(1);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return -1;
    }

    /**
     * Vide puis rempli la liste des ingrédients disponibles à partir de la BD(Pour l'initialiser, puis la rafraîchir)
     */
    void fillIngList()
    {
        arrayListIng.clear();
        Statement stm1;
        ResultSet resultSet;
        String sql="select NOMBOUTEILLE,DESCRIPTIONS from INGREDIENT";
        try {
            stm1 = conn_.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE,ResultSet.CONCUR_READ_ONLY);

            resultSet = stm1.executeQuery(sql);
            while(resultSet.next())
            {
                HashMap<String,String> hashMap=new HashMap<>();//create a hashmap to store the data in key value pair

                hashMap.put("nom",resultSet.getString(1));
                hashMap.put("desc",resultSet.getString(2));
                arrayListIng.add(hashMap);//add the hashmap into arrayList
            }

        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    void refreshIngList()
    {
        SimpleAdapter simpleAdapter=new SimpleAdapter(this,arrayListIng,R.layout.custom_list_ing,from,toIng);
        SimpleAdapter.ViewBinder binder = new SimpleAdapter.ViewBinder() {
            @Override
            public boolean setViewValue(View view, Object object, String value) {

                if (view.equals((TextView) view.findViewById(R.id.nameIng_TXT))) {
                    TextView nomTXT = (TextView) view.findViewById(R.id.nameIng_TXT);
                    if(radioGRP.getCheckedRadioButtonId()==R.id.changerBlanc_RBTN) {
                        nomTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if(radioGRP.getCheckedRadioButtonId()==R.id.changerNoir_RBTN) {
                        nomTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if(radioGRP.getCheckedRadioButtonId()==R.id.changerJelly_RBTN) {
                        nomTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                if (view.equals((TextView) view.findViewById(R.id.descIng_TXT))) {
                    TextView descTXT = (TextView) view.findViewById(R.id.descIng_TXT);
                    if (radioGRP.getCheckedRadioButtonId() == R.id.changerBlanc_RBTN) {
                        descTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if (radioGRP.getCheckedRadioButtonId() == R.id.changerNoir_RBTN) {
                        descTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if (radioGRP.getCheckedRadioButtonId() == R.id.changerJelly_RBTN) {
                        descTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                return false;
            }
        };

        simpleAdapter.setViewBinder(binder);
        listIngLVIEW.setAdapter(simpleAdapter);//sets the adapter for listView
    }

    void refreshItemCourant()
    {
        SimpleAdapter simpleAdapter=new SimpleAdapter(this,arrayListItemCourant,R.layout.custom_list_itemcourant,from,toCourant);
        SimpleAdapter.ViewBinder binder = new SimpleAdapter.ViewBinder() {
            @Override
            public boolean setViewValue(View view, Object object, String value) {

                if (view.equals((TextView) view.findViewById(R.id.nameCourant_TXT))) {
                    TextView nomTXT = (TextView) view.findViewById(R.id.nameCourant_TXT);
                    if(radioGRP.getCheckedRadioButtonId()==R.id.changerBlanc_RBTN) {
                        nomTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if(radioGRP.getCheckedRadioButtonId()==R.id.changerNoir_RBTN) {
                        nomTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if(radioGRP.getCheckedRadioButtonId()==R.id.changerJelly_RBTN) {
                        nomTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                if (view.equals((TextView) view.findViewById(R.id.descCourant_TXT))) {
                    TextView descTXT = (TextView) view.findViewById(R.id.descCourant_TXT);
                    if (radioGRP.getCheckedRadioButtonId() == R.id.changerBlanc_RBTN) {
                        descTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if (radioGRP.getCheckedRadioButtonId() == R.id.changerNoir_RBTN) {
                        descTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if (radioGRP.getCheckedRadioButtonId() == R.id.changerJelly_RBTN) {
                        descTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                if(view.equals((TextView) view.findViewById(R.id.noteCourant_TXT))) {
                    TextView noteTXT = (TextView) view.findViewById(R.id.noteCourant_TXT);
                    if (radioGRP.getCheckedRadioButtonId() == R.id.changerBlanc_RBTN) {
                        noteTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if (radioGRP.getCheckedRadioButtonId() == R.id.changerNoir_RBTN) {
                        noteTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if (radioGRP.getCheckedRadioButtonId() == R.id.changerJelly_RBTN) {
                        noteTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                return false;
            }
        };
        simpleAdapter.setViewBinder(binder);

        drinkItemLVIEW.setAdapter(simpleAdapter);//sets the adapter for listView
    }

    /**
     * Permet d'initialiser et rafraîchir la liste du panier
     */
    void fillCartList()
    {
        final ImageButton commandBTN=findViewById(R.id.command_IMGBTN);
        if(arrayListCart.size()!=0)
            commandBTN.setVisibility(View.VISIBLE);
        else
            commandBTN.setVisibility(View.INVISIBLE);

        SimpleAdapter simpleAdapter=new SimpleAdapter(this,arrayListCart,R.layout.custom_list_ing,from,toIng);

        SimpleAdapter.ViewBinder binder = new SimpleAdapter.ViewBinder() {
            @Override
            public boolean setViewValue(View view, Object object, String value) {

                if (view.equals((TextView) view.findViewById(R.id.nameIng_TXT))) {
                    TextView nomTXT = (TextView) view.findViewById(R.id.nameIng_TXT);
                    if(radioGRP.getCheckedRadioButtonId()==R.id.changerBlanc_RBTN) {
                        nomTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if(radioGRP.getCheckedRadioButtonId()==R.id.changerNoir_RBTN) {
                        nomTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if(radioGRP.getCheckedRadioButtonId()==R.id.changerJelly_RBTN) {
                        nomTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                if (view.equals((TextView) view.findViewById(R.id.descIng_TXT))) {
                    TextView descTXT = (TextView) view.findViewById(R.id.descIng_TXT);
                    if (radioGRP.getCheckedRadioButtonId() == R.id.changerBlanc_RBTN) {
                        descTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if (radioGRP.getCheckedRadioButtonId() == R.id.changerNoir_RBTN) {
                        descTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if (radioGRP.getCheckedRadioButtonId() == R.id.changerJelly_RBTN) {
                        descTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                return false;
            }
        };
        simpleAdapter.setViewBinder(binder);

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

    void Commander()
    {
        HashMap<String, Integer> drink= new HashMap<>();
        for (int i = 0; i < arrayListCart.size(); i++)
        {
            drink=défaireDescription(arrayListCart.get(i).get("desc"));
            try {
                Statement statement = conn_.createStatement();
                statement.executeUpdate("INSERT INTO COMMANDE " + "VALUES (1001, 'Simpson', 'Mr.', 'Springfield', 2001)");
            } catch (SQLException e) {
                e.printStackTrace();
            }

        }

        DemanderNote(arrayListCart.get(0).get("nom"));
        faireToast("Merci de votre commande. Veuillez noter s'il vous plait.");
        selectedCartPositions.clear();
        fillCartList();
        fillDrinksList();
    }

    void DemanderNote(String nomMix)
    {
        final TextView nomMixTXT=findViewById(R.id.nomMix_TXT);

        nomMixTXT.setText(nomMix);
        notesLYT.setVisibility(View.VISIBLE);
        fillCartList();
    }

    void AnnulerNote() {

        final TextView nomMixTXT=findViewById(R.id.nomMix_TXT);
        nomMixTXT.setText("");

        ReinitTableauNotes();
        arrayListCart.remove(0);
        if(arrayListCart.size()!=0) {
            DemanderNote(arrayListCart.get(0).get("nom"));
        }
        else
            fillCartList();
        refreshCartItemCount();
    }

    void EnvoyerNote() {

        if(note==0)
            faireToast("Désolé de votre mauvaise expérience. Revenez nous voir.");
        else if(note==1)
            faireToast("Merci d'avoir noté: "+note+" étoile");
        else
            faireToast("Merci d'avoir noté: "+note+" étoiles");

        //ENVOYER ICI A LA BD arrayListCart.get(0).get("nom") et note
        {

            fillDrinksList();
        }


        ReinitTableauNotes();
        arrayListCart.remove(0);
        if(arrayListCart.size()!=0) {
            DemanderNote(arrayListCart.get(0).get("nom"));
        }
        else
            fillCartList();
        refreshCartItemCount();
    }

    void ReinitTableauNotes()
    {
        final ImageButton etoile1= findViewById(R.id.star1_IMGBTN);
        final ImageButton etoile2= findViewById(R.id.star2_IMGBTN);
        final ImageButton etoile3= findViewById(R.id.star3_IMGBTN);
        final ImageButton etoile4= findViewById(R.id.star4_IMGBTN);
        final ImageButton etoile5= findViewById(R.id.star5_IMGBTN);
        etoile1.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
        etoile2.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
        etoile3.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
        etoile4.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
        etoile5.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
        notesLYT.setVisibility(View.INVISIBLE);
        note=0;
    }

    void TrierEtoileHaut()
    {
        final TextView triNoteBTN=findViewById(R.id.triNote_BTN);
        triNoteBTN.setText("▲");
        Collections.sort(arrayListDrink, new Comparator<HashMap<String,String>>()
        {
            public int compare(HashMap<String,String> o1,
                               HashMap<String,String> o2)
            {
                    if(!o1.get("note").equals("NA")&&!o2.get("note").equals("NA")) {
                        float o1note = Float.valueOf(o1.get("note"));
                        float o2note = Float.valueOf(o2.get("note"));
                        if (o1note < o2note) {
                            return -1;
                        } else if (o1note > o2note) {
                            return 1;
                        }
                    }
                    else if(!o1.get("note").equals("NA")&&o2.get("note").equals("NA"))
                    {
                        return -1;
                    }
                    else if(o1.get("note").equals("NA")&&!o2.get("note").equals("NA"))
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
        final TextView triNoteBTN=findViewById(R.id.triNote_BTN);
        triNoteBTN.setText("▼");
        Collections.sort(arrayListDrink, new Comparator<HashMap<String,String>>()
        {
            public int compare(HashMap<String,String> o1,
                               HashMap<String,String> o2)
            {
                    if(!o1.get("note").equals("NA")&&!o2.get("note").equals("NA")) {
                        float o1note = Float.valueOf(o1.get("note"));
                        float o2note = Float.valueOf(o2.get("note"));
                        if (o1note < o2note) {
                            return 1;
                        } else if (o1note > o2note) {
                            return -1;
                        }
                    }
                    else if(!o1.get("note").equals("NA")&&o2.get("note").equals("NA"))
                    {
                        return -1;
                    }
                    else if(o1.get("note").equals("NA")&&!o2.get("note").equals("NA"))
                    {
                        return 1;
                    }
                    return 0;
                }
        });
        refreshDrinkList();
    }

    void EnleverTri()
    {
        final TextView triNoteBTN=findViewById(R.id.triNote_BTN);
        triNoteBTN.setText("A-B");
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

    void refreshCartItemCount()
    {
        final TextView itemCountTXT=findViewById(R.id.cartItemsCount_TXT);
        itemCountTXT.setText(Integer.toString(arrayListCart.size()));
        final TextView panierTXT=findViewById(R.id.cart_TXT);
        if(arrayListCart.size()==0)

            panierTXT.setText(getResources().getString(R.string.cartempty_str));
        else {
            panierTXT.setText(getResources().getString(R.string.cart_str));
            panierTXT.setPaintFlags(panierTXT.getPaintFlags() |   Paint.UNDERLINE_TEXT_FLAG);
        }
    }

    void faireToast(String message)
    {
        Toast toast = Toast.makeText(getApplicationContext(),
                message, Toast.LENGTH_LONG);
        toast.setGravity(Gravity.CENTER, 0, toast_height);
        View view = toast.getView();

        view.setBackgroundColor(getResources().getColor(R.color.yellow));
        toast.show();
    }

    HashMap<String, Integer> défaireDescription(String description)
    {
        HashMap<String, Integer> ingredients = new HashMap<>();
        ArrayList<HashMap<String, Integer>> drink = new ArrayList<>();
        ArrayList<String> tableauIngredients=new ArrayList<>();
        String line=description;

        while(line.contains(","))
        {
            tableauIngredients.add(line.substring(0, line.indexOf(",")));
            line=line.substring(line.indexOf(",") + 1);
        }
        tableauIngredients.add(line);

        for(int i=0;i<tableauIngredients.size();i++)
        {
            String ElementCommande[] = tableauIngredients.get(i).split("oz");
            ingredients.put(ElementCommande[1].trim(), Integer.valueOf(ElementCommande[0].trim()));
        }
        return ingredients;
    }

    String faireDescription(HashMap<String, Integer> ingredients)
    {
        Set keys = ingredients.keySet();
        Collection<Integer> items=ingredients.values();
        String desc="";
        for (Iterator i = keys.iterator(); i.hasNext(); )
        {
            String key = (String) i.next();
            Integer value = ingredients.get(key);
            if(i.hasNext())
                desc+=value+" oz "+key+", ";
            else
                desc+=value+" oz "+key;
        }
        return desc;
    }

    String arrondir(float nombre)
    {
        DecimalFormat df = new DecimalFormat("#.##");
        df.setRoundingMode(RoundingMode.DOWN);
        return df.format(nombre);
    }

    void changerBlanc()
    {
        ColorStateList colorRBTN = new ColorStateList(
                new int[][]{
                        new int[]{-android.R.attr.state_checked},
                        new int[]{android.R.attr.state_checked}
                },
                new int[]{

                        getResources().getColor(R.color.grey)
                        , getResources().getColor(R.color.yellow)
                }
        );

        findViewById(R.id.background_LYT).setBackgroundColor(getResources().getColor(R.color.white));
        findViewById(R.id.backgroundFooter_TView).setBackgroundColor(getResources().getColor(R.color.black));

        changeMenuButtonsColor(couleurs.get("blanc"));
        changeTextColor(couleurs.get("noir"));
        changeRadioButtonColor(colorRBTN);
    }

    void changerNoir()
    {
        ColorStateList colorRBTN = new ColorStateList(
                new int[][]{
                        new int[]{-android.R.attr.state_checked},
                        new int[]{android.R.attr.state_checked}
                },
                new int[]{

                        getResources().getColor(R.color.white)
                        , getResources().getColor(R.color.white)
                }
        );

        findViewById(R.id.background_LYT).setBackgroundColor(getResources().getColor(R.color.black));
        findViewById(R.id.backgroundFooter_TView).setBackgroundColor(getResources().getColor(R.color.white));

        changeMenuButtonsColor(couleurs.get("noir"));
        changeTextColor(couleurs.get("blanc"));
        changeRadioButtonColor(colorRBTN);
    }

    void changerJELLY()
    {
        ColorStateList colorRBTN = new ColorStateList(
                new int[][]{
                        new int[]{-android.R.attr.state_checked},
                        new int[]{android.R.attr.state_checked}
                },
                new int[]{

                        getResources().getColor(R.color.black)
                        , getResources().getColor(R.color.black)
                }
        );
        findViewById(R.id.background_LYT).setBackgroundColor(getResources().getColor(R.color.yellow));
        findViewById(R.id.backgroundFooter_TView).setBackgroundColor(getResources().getColor(R.color.black));

        changeMenuButtonsColor(couleurs.get("jaune"));
        changeTextColor(couleurs.get("blanc"));
        changeRadioButtonColor(colorRBTN);
    }

    void changeMenuButtonsColor(ColorStateList color)
    {
        drinkBTN.setBackgroundTintList(color);
        cartBTN.setBackgroundTintList(color);
        optionsBTN.setBackgroundTintList(color);
        infoBTN.setBackgroundTintList(color);
    }

    void changeTextColor(ColorStateList color)
    {
        TextView optionLBL=findViewById(R.id.options_TXT);
        RadioButton noirRBTN = findViewById(R.id.changerNoir_RBTN);
        RadioButton blancRBTN = findViewById(R.id.changerBlanc_RBTN);
        RadioButton jellyRBTN = findViewById(R.id.changerJelly_RBTN);

        TextView infosLBL=findViewById(R.id.infos_TXT);
        TextView infosTXT=findViewById(R.id.informations_TXT);

        blancRBTN.setTextColor(color);
        noirRBTN.setTextColor(color);
        jellyRBTN.setTextColor(color);
        optionLBL.setTextColor(color);

        infosLBL.setTextColor(color);
        infosTXT.setTextColor(color);
    }

    void changeRadioButtonColor(ColorStateList color)
    {
        RadioButton noirRBTN = findViewById(R.id.changerNoir_RBTN);
        RadioButton blancRBTN = findViewById(R.id.changerBlanc_RBTN);
        RadioButton jellyRBTN = findViewById(R.id.changerJelly_RBTN);

        blancRBTN.setButtonTintList(color);
        noirRBTN.setButtonTintList(color);
        jellyRBTN.setButtonTintList(color);
    }

}