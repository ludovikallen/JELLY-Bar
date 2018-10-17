package com.example.a201625221.projetjelly;

//region Imports
import android.annotation.SuppressLint;
import android.content.res.ColorStateList;
import android.graphics.Paint;
import android.graphics.PorterDuff;
import android.graphics.Rect;
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
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;
import android.os.StrictMode;

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
//endregion

public class MainActivity extends AppCompatActivity {
    public static Connection conn_ = null;
    static int hauteur_toast =420;

    /**
     * Variables pour contenir les layouts pour pouvoir changer d'onglet dans l'application
     */
    ConstraintLayout listeDrinkShotLYT, modifierLYT,optionsLYT, panierLYT,infosLYT,notesLYT,connexionLYT;

    /**
     * Variables pour contenir les boutons pour pouvoir changer d'onglet dans l'application
     */
    Button drinkBTN, panierBTN,optionsBTN, infosBTN;

    /**
     * Variables permettant d'afficher dans la ListView les éléments des ArrayList<HashMap<String,String>> en passant par l'adapter
     */
    ListView listeDrinksLVIEW, listeShootersLVIEW, listeIngredientsLVIEW, panierLVIEW, drinkItemLVIEW;

    /**
     * Tableaux pour indiquer l'origine des données de l'adapter
     */
    String from[]={"nom","desc","note"};

    /**
     * Tableau contenant la destination graphique de l'adapter pour la liste des drinks
     */
    int toDrink[]={R.id.nameDrink_TXT,R.id.descDrink_TXT,R.id.noteDrink_TXT};

    /**
     * Tableau contenant la destination graphique de l'adapter pour la liste des ingrédients/panier
     */
    int toIng[]={R.id.nameIng_TXT,R.id.descIng_TXT};

    /**
     * Tableau contenant la destination graphique de l'adapter pour le drink en cours de modification
     */
    int toCourant[]={R.id.nameCourant_TXT,R.id.descCourant_TXT,R.id.noteCourant_TXT};

    /**
     * Listes qui vont être les contenants des éléments de la BD et le panier
     */
    ArrayList<HashMap<String,String>> arrayListDrink =new ArrayList<>(),arrayListIng=new ArrayList<>(), arrayListPanier =new ArrayList<>(), arrayListItemCourant=new ArrayList<>();

    /**
     * Liste contenant les éléments sélectionnés du panier
     */
    ArrayList<Integer> selectionPositionsPanier = new ArrayList<>();

    /**
     * Radiogroup de sélection de couleur
     */
    RadioGroup couleursRDGRP;

    /**
     * Hashmap où les couleurs principales du programme seront stockées
     */
    HashMap<String, ColorStateList> couleurs=new HashMap<>();

    /**
     * Couleur choisie lors de l'ouverture de l'application
     */
    String couleurChoisie="blanc";

    /**
     * Index dans la liste de l'article en cours de modification
     */
    int indexItemModification=-1;

    /**
     * Note présentement choisie pour le drink courant
     */
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

                    Thread thread = new Thread(){
                        public void run(){
                            Initialiser();
                        }
                    };

                    runOnUiThread(thread);
                }
                catch (java.sql.SQLException se)
                {
                    faireToast("Connexion au serveur  impossible." + se.getMessage());
                }
            }
        };
        t.start();
    }

    //region Initialiser

    /**
     * Appel de toutes les fonctions d'initialisation
     */
    void Initialiser()
    {
        InitialiserComposantes();
        InitialiserListes();
        InitialiserCouleurs();
        setTouchListeners();
        setClickListeners();
        setCheckedListeners();
    }

    /**
     * Initialise les composantes globales utilisées plusieurs fois pour ne pas avoir à les rechercher dans les fonctions
     */
     void InitialiserComposantes()
    {
        listeDrinkShotLYT =findViewById(R.id.listDrink_LYT);
        modifierLYT =findViewById(R.id.listIng_LYT);
        optionsLYT=findViewById(R.id.options_LYT);
        panierLYT =findViewById(R.id.cart_LYT);
        infosLYT=findViewById(R.id.infos_LYT);
        notesLYT=findViewById(R.id.notes_LYT);
        connexionLYT=findViewById(R.id.connexion_LYT);

        drinkBTN=findViewById(R.id.drinklist_BTN);
        infosBTN =findViewById(R.id.infos_BTN);
        panierBTN =findViewById(R.id.cart_BTN);
        optionsBTN=findViewById(R.id.options_BTN);

        listeDrinksLVIEW =findViewById(R.id.drink_LVIEW);
        listeShootersLVIEW=findViewById(R.id.shooter_LVIEW);
        listeIngredientsLVIEW =findViewById(R.id.ing_LVIEW);
        panierLVIEW =findViewById(R.id.cart_LVIEW);
        drinkItemLVIEW=findViewById(R.id.drinkItem_LVIEW);

        couleursRDGRP =findViewById(R.id.changerCouleur_RBTNGRP);
    }

    /**
     * Initialise les trois listes principales, puis les remplis à partir de la BD
     */
    void InitialiserListes()
    {
        remplirListeDrinks();
        remplirListeIngredients();
        remplirListePanier();

        rafraichirListeDrinks();
        rafraichirListeIngredients();
        rafraichirListeShooters();
        rafraichirItemCourant();
    }

    void InitialiserCouleurs()
    {
        int[][] etats = new int[][] {
                new int[] { }
        };
        int[] couleurs = new int[] {
                getResources().getColor(R.color.yellow),
        };

        ColorStateList jaune = new ColorStateList(etats, couleurs);
        this.couleurs.put("jaune",jaune);

        etats = new int[][] {
                new int[] { }
        };
        couleurs = new int[] {
                getResources().getColor(R.color.black),
        };

        ColorStateList noir = new ColorStateList(etats, couleurs);
        this.couleurs.put("noir",noir);

        etats = new int[][] {
                new int[] { }
        };
        couleurs = new int[] {
                getResources().getColor(R.color.white),
        };

        ColorStateList blanc = new ColorStateList(etats, couleurs);
        this.couleurs.put("blanc",blanc);
    }

    //endregion

    //region setListeners

    /**
     * Initialise les touch listeners, pour effectuer des actions avant le relâchement du toucher
     */
    @SuppressLint("ClickableViewAccessibility")
    void setTouchListeners()
    {
        final ImageButton supprimerBTN=findViewById(R.id.trash_IMGBTN);
        final ImageButton commanderBTN=findViewById(R.id.command_IMGBTN);
        final TextView quitterNotesBTN=findViewById(R.id.exitNoteBTN);

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

        infosBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                infosBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.grey)));
                infosBTN.setBackgroundResource(R.drawable.iconinfo);
                return false;
            }
        });

        panierBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                panierBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.grey)));
                panierBTN.setBackgroundResource(R.drawable.iconcart);
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

        supprimerBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                supprimerBTN.setBackgroundColor(getResources().getColor(R.color.grey));
                return false;
            }
        });

        quitterNotesBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                quitterNotesBTN.setBackgroundColor(getResources().getColor(R.color.darkgrey));
                return false;
            }
        });
        commanderBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                commanderBTN.setBackgroundColor(getResources().getColor(R.color.grey));
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
        final ImageButton supprimerBTN=findViewById(R.id.trash_IMGBTN);
        final TextView supprimerToutBTN=findViewById(R.id.trashall_BTN);
        final ImageButton commanderBTN=findViewById(R.id.command_IMGBTN);
        final TextView quitterNotesBTN=findViewById(R.id.exitNoteBTN);
        final TextView envoyerNoteBTN=findViewById(R.id.sendNote_BTN);
        final TextView triNotesBTN=findViewById(R.id.triNote_BTN);
        final Button accepterChangementsBTN=findViewById(R.id.acceptChange_BTN);
        final Button annulerChangementsBTN=findViewById(R.id.cancelChange_BTN);
        final Button connecterBTN= findViewById(R.id.connexion_BTN);

        final ImageButton etoile1= findViewById(R.id.star1_IMGBTN);
        final ImageButton etoile2= findViewById(R.id.star2_IMGBTN);
        final ImageButton etoile3= findViewById(R.id.star3_IMGBTN);
        final ImageButton etoile4= findViewById(R.id.star4_IMGBTN);
        final ImageButton etoile5= findViewById(R.id.star5_IMGBTN);

        drinkBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if(couleurChoisie.equals("blanc"))
                    changerCouleurBoutonsMenu(couleurs.get("blanc"));
                else if(couleurChoisie.equals("noir"))
                    changerCouleurBoutonsMenu(couleurs.get("noir"));
                else if(couleurChoisie.equals("jaune"))
                    changerCouleurBoutonsMenu(couleurs.get("jaune"));
                drinkBTN.setBackgroundResource(R.drawable.icondrink);

                listeDrinkShotLYT.setVisibility(View.VISIBLE);
                modifierLYT.setVisibility(View.INVISIBLE);
                optionsLYT.setVisibility(View.INVISIBLE);
                panierLYT.setVisibility(View.INVISIBLE);
                infosLYT.setVisibility(View.INVISIBLE);

                rafraichirListeDrinks();
                rafraichirListeIngredients();
                rafraichirListeShooters();
                rafraichirItemCourant();
                remplirListeDrinks();
                remplirListeIngredients();
                remplirListePanier();
                enleverTri();
            }
        });

        panierBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if(couleurChoisie.equals("blanc"))
                    changerCouleurBoutonsMenu(couleurs.get("blanc"));
                else if(couleurChoisie.equals("noir"))
                    changerCouleurBoutonsMenu(couleurs.get("noir"));
                else if(couleurChoisie.equals("jaune"))
                    changerCouleurBoutonsMenu(couleurs.get("jaune"));
                panierBTN.setBackgroundResource(R.drawable.iconcart);

                listeDrinkShotLYT.setVisibility(View.INVISIBLE);
                modifierLYT.setVisibility(View.INVISIBLE);
                optionsLYT.setVisibility(View.INVISIBLE);
                panierLYT.setVisibility(View.VISIBLE);
                infosLYT.setVisibility(View.INVISIBLE);
            }
        });

        optionsBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if(couleurChoisie.equals("blanc"))
                    changerCouleurBoutonsMenu(couleurs.get("blanc"));
                else if(couleurChoisie.equals("noir"))
                    changerCouleurBoutonsMenu(couleurs.get("noir"));
                else if(couleurChoisie.equals("jaune"))
                    changerCouleurBoutonsMenu(couleurs.get("jaune"));
                optionsBTN.setBackgroundResource(R.drawable.iconoptions);

                listeDrinkShotLYT.setVisibility(View.INVISIBLE);
                modifierLYT.setVisibility(View.INVISIBLE);
                optionsLYT.setVisibility(View.VISIBLE);
                panierLYT.setVisibility(View.INVISIBLE);
                infosLYT.setVisibility(View.INVISIBLE);
            }
        });

        infosBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if(couleurChoisie.equals("blanc"))
                    changerCouleurBoutonsMenu(couleurs.get("blanc"));
                else if(couleurChoisie.equals("noir"))
                    changerCouleurBoutonsMenu(couleurs.get("noir"));
                else if(couleurChoisie.equals("jaune"))
                    changerCouleurBoutonsMenu(couleurs.get("jaune"));
                infosBTN.setBackgroundResource(R.drawable.iconinfo);

                listeDrinkShotLYT.setVisibility(View.INVISIBLE);
                modifierLYT.setVisibility(View.INVISIBLE);
                optionsLYT.setVisibility(View.INVISIBLE);
                panierLYT.setVisibility(View.INVISIBLE);
                infosLYT.setVisibility(View.VISIBLE);
            }
        });

        supprimerBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                RetirerPanier();
                supprimerBTN.setVisibility(View.INVISIBLE);
                afficherNombreItemsPanier();
            }
        });

        supprimerToutBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                selectionPositionsPanier.clear();
                arrayListPanier.clear();
                remplirListePanier();
                supprimerBTN.setVisibility(View.INVISIBLE);
                afficherNombreItemsPanier();
            }
        });

        commanderBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                commanderBTN.setBackgroundColor(getResources().getColor(R.color.white));
                commander();
            }
        });

        quitterNotesBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                quitterNotesBTN.setBackgroundColor(getResources().getColor(R.color.grey));
                annulerNote();
            }
        });

        envoyerNoteBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                envoyerNote();
            }
        });

        triNotesBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if(triNotesBTN.getText().equals("▼"))
                {
                    trierHaut();
                }
                else if(triNotesBTN.getText().equals("▲"))
                {
                    enleverTri();
                }
                else if(triNotesBTN.getText().equals("A-B"))
                {
                    trierBas();
                }
            }
        });

        accepterChangementsBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if(indexItemModification!=-1) {
                    arrayListPanier.remove(indexItemModification);
                }
                indexItemModification=-1;
                arrayListPanier.add(arrayListItemCourant.get(0));
                arrayListItemCourant.clear();
                modifierLYT.setVisibility(View.INVISIBLE);
                panierLYT.setVisibility(View.VISIBLE);
                afficherNombreItemsPanier();
                remplirListePanier();
            }
        });

        annulerChangementsBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                indexItemModification=-1;
                arrayListItemCourant.clear();
                modifierLYT.setVisibility(View.INVISIBLE);
                listeDrinkShotLYT.setVisibility(View.VISIBLE);
            }
        });

        connecterBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                connexionLYT.setVisibility(View.INVISIBLE);
                drinkBTN.setVisibility(View.VISIBLE);
                panierBTN.setVisibility(View.VISIBLE);
                optionsBTN.setVisibility(View.VISIBLE);
                infosBTN.setVisibility(View.VISIBLE);
                OracleConnexion();
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


        listeDrinksLVIEW.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int position,
                                    long id) {

                HashMap<String, String> item = ( HashMap<String, String>)adapterView.getItemAtPosition(position);
                faireToast("x1 " + item.values().toArray()[1] + " ajouté au panier");

                ajouterPanier(item);
            }
        });

        listeIngredientsLVIEW.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int position,
                                    long id) {

                HashMap<String, String> nouveauIngredient = ( HashMap<String, String>)adapterView.getItemAtPosition(position);
                faireToast("x1 " + nouveauIngredient.values().toArray()[0] + " ajouté au drink");

                HashMap<String, String> itemActuel=arrayListItemCourant.get(0);
                arrayListItemCourant.clear();
                HashMap<String, String> nouvelItemActuel=new HashMap<String, String>();

                HashMap<String, Integer> ingredients= defaireDescription(itemActuel.get("desc"));
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
                rafraichirItemCourant();
            }
        });

        panierLVIEW.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int position,
                                    long id) {
                if(view != null && view.getContext() != null) {
                    supprimerBTN.setVisibility(View.VISIBLE);

                    if (arrayListPanier.size() < position)
                        selectionPositionsPanier.add(0);
                    if (selectionPositionsPanier.contains(position))
                        selectionPositionsPanier.remove(selectionPositionsPanier.indexOf(position));
                    else if (!selectionPositionsPanier.contains(position))
                        selectionPositionsPanier.add(position);

                    if (selectionPositionsPanier.size() > 0) {
                        if (arrayListPanier.size() < position)
                            panierLVIEW.getChildAt(0).setBackgroundColor(getResources().getColor(R.color.listSelector));
                        if (selectionPositionsPanier.contains(position))
                            panierLVIEW.getChildAt(position).setBackgroundColor(getResources().getColor(R.color.listSelector));
                        else {
                            if(couleurChoisie.equals("blanc"))
                                panierLVIEW.getChildAt(position).setBackgroundColor(couleurs.get("blanc").getDefaultColor());
                            else if(couleurChoisie.equals("noir"))
                                panierLVIEW.getChildAt(position).setBackgroundColor(couleurs.get("noir").getDefaultColor());
                            else if(couleurChoisie.equals("jaune"))
                                panierLVIEW.getChildAt(position).setBackgroundColor(couleurs.get("jaune").getDefaultColor());
                        }
                    }
                }
            }
        });

        listeDrinksLVIEW.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {

            public boolean onItemLongClick(AdapterView<?> adapterView, View view, int position, long id) {
                    arrayListItemCourant.clear();
                    HashMap<String, String> item = ( HashMap<String, String>)adapterView.getItemAtPosition(position);
                    arrayListItemCourant.add(item);
                    rafraichirItemCourant();

                    listeDrinkShotLYT.setVisibility(View.INVISIBLE);
                    modifierLYT.setVisibility(View.VISIBLE);
                    remplirListeIngredients();
                    rafraichirListeIngredients();
                return true;
            }
        });

        panierLVIEW.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {

            public boolean onItemLongClick(AdapterView<?> adapterView, View view, int position, long id) {
                    arrayListItemCourant.clear();
                    HashMap<String, String> item = ( HashMap<String, String>)adapterView.getItemAtPosition(position);
                    arrayListItemCourant.add(item);
                    rafraichirItemCourant();

                    panierLYT.setVisibility(View.INVISIBLE);
                    modifierLYT.setVisibility(View.VISIBLE);
                    remplirListeIngredients();
                    rafraichirListeIngredients();
                    indexItemModification=position;
                return true;
            }
        });

        listeIngredientsLVIEW.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {

            public boolean onItemLongClick(AdapterView<?> adapterView, View view, int position,
                                    long id) {

                HashMap<String, String> nouveauIngredient = ( HashMap<String, String>)adapterView.getItemAtPosition(position);

                HashMap<String, String> itemActuel=arrayListItemCourant.get(0);
                HashMap<String, Integer> ingredients= defaireDescription(itemActuel.get("desc"));
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
                rafraichirItemCourant();
                return true;
            }
        });
    }

    public void setCheckedListeners()
    {
        couleursRDGRP.setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener()
        {
            @Override
            public void onCheckedChanged(RadioGroup group, int checkedId) {
                if(checkedId==R.id.changerBlanc_RBTN)
                {
                    changerBlanc();
                    couleurChoisie="blanc";
                }
                else if(checkedId==R.id.changerNoir_RBTN)
                {
                    changerNoir();
                    couleurChoisie="noir";
                }
                else if(checkedId==R.id.changerJelly_RBTN)
                {
                    changerJELLY();
                    couleurChoisie="jaune";
                }
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
                remplirListePanier();
                arrayListPanier.clear();
                afficherNombreItemsPanier();

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

    //endregion

    //region Panier

    void commander()
    {
        HashMap<String, Integer> drink;
        String sql2 = "Select max(numcommande) from commande";
        int Numcommande=0;
        ResultSet resultSetMax = null;
        try {
            Statement stm12 = conn_.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_READ_ONLY);
            resultSetMax = stm12.executeQuery(sql2);
            resultSetMax.next();
            Numcommande = resultSetMax.getInt(1) + 1;
        } catch (SQLException e) {
            e.printStackTrace();
        }
        for (int i = 0; i < arrayListPanier.size(); i++)
        {
            Statement stm1;
            ResultSet resultSet;
            drink= defaireDescription(arrayListPanier.get(i).get("desc"));
            for ( String key : drink.keySet() ) {
                Object value = drink.get(key);

                String sql = "select codebouteille from INGREDIENT where nombouteille = '" + key +"'";
                try {
                    stm1 = conn_.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_READ_ONLY);
                    resultSet = stm1.executeQuery(sql);
                    resultSet.next();
                    Statement statement = conn_.createStatement();
                    statement.executeUpdate("INSERT INTO COMMANDE VALUES ( "+( Numcommande + i) + ","+ resultSet.getInt(1) +","+ value +")");
                } catch (SQLException e) {
                    e.printStackTrace();
                }
            }
        }

        demanderNote(arrayListPanier.get(0).get("nom"));
        faireToast("Merci de votre commande. Veuillez noter s'il vous plait.");
        selectionPositionsPanier.clear();
        remplirListePanier();
        remplirListeDrinks();
    }

    /**
     * Ajoute l'élément envoyé en paramètre dans le panier
     */
    void ajouterPanier(HashMap<String, String> ajout)
    {
        arrayListPanier.add(ajout);
        remplirListePanier();
        afficherNombreItemsPanier();
    }

    /**
     * Supprime l'élément envoyé en paramètre du panier
     */
    @Deprecated
    void RetirerPanier(HashMap<String, String> retrait)
    {
        if(retrait!=null) {
            arrayListPanier.remove(retrait);
            remplirListePanier();
            faireToast("x1 " + retrait.values().toArray()[0] + " retiré du panier");
        }
    }

    /**
     * Supprime les éléments courants selectionnés du panier
     */
    void RetirerPanier()
    {
        int compteurItems=0;
        for (; compteurItems< selectionPositionsPanier.size(); compteurItems++)
        {
            arrayListPanier.set(selectionPositionsPanier.get(compteurItems),null);
        }
        while(arrayListPanier.remove(null));
        selectionPositionsPanier.clear();
        remplirListePanier();
        if(compteurItems==1)
            faireToast(compteurItems + " item retiré du panier");
        else
            faireToast(compteurItems + " items retirés du panier");
        afficherNombreItemsPanier();
    }

    void afficherNombreItemsPanier()
    {
        final TextView itemCountTXT=findViewById(R.id.cartItemsCount_TXT);
        itemCountTXT.setText(Integer.toString(arrayListPanier.size()));
        final TextView panierTXT=findViewById(R.id.cart_TXT);
        if(arrayListPanier.size()==0)

            panierTXT.setText(getResources().getString(R.string.cartempty_str));
        else {
            panierTXT.setText(getResources().getString(R.string.cart_str));
            panierTXT.setPaintFlags(panierTXT.getPaintFlags() |   Paint.UNDERLINE_TEXT_FLAG);
        }
    }

    //endregion

    //region Remplir listes

    /**
     * Vide puis rempli la liste des drinks disponibles à partir de la BD(Pour l'initialiser, puis la rafraîchir)
     */
    void remplirListeDrinks() {
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
                if(Notetrouver != null&&!Notetrouver.trim().equals(""))
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
        enleverTri();
    }

    void rafraichirListeDrinks()
    {
        SimpleAdapter simpleAdapter=new SimpleAdapter(this, arrayListDrink,R.layout.custom_list_drink,from,toDrink);
        SimpleAdapter.ViewBinder binder = new SimpleAdapter.ViewBinder() {
            @Override
            public boolean setViewValue(View view, Object object, String value) {

                if (view.equals((TextView) view.findViewById(R.id.nameDrink_TXT))) {
                    TextView nomTXT = (TextView) view.findViewById(R.id.nameDrink_TXT);
                    if(couleurChoisie.equals("blanc")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if(couleurChoisie.equals("noir")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if(couleurChoisie.equals("jaune")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                if (view.equals((TextView) view.findViewById(R.id.descDrink_TXT))) {
                    TextView descTXT = (TextView) view.findViewById(R.id.descDrink_TXT);
                    if (couleurChoisie.equals("blanc")) {
                        descTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if (couleurChoisie.equals("noir")) {
                        descTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if (couleurChoisie.equals("jaune")) {
                        descTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                if(view.equals((TextView) view.findViewById(R.id.noteDrink_TXT))) {
                    TextView noteTXT = (TextView) view.findViewById(R.id.noteDrink_TXT);
                    if (couleurChoisie.equals("blanc")) {
                        noteTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if (couleurChoisie.equals("noir")) {
                        noteTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if (couleurChoisie.equals("jaune")) {
                        noteTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                return false;
            }
        };
        simpleAdapter.setViewBinder(binder);

        listeDrinksLVIEW.setAdapter(simpleAdapter);//sets the adapter for listView
    }

    /**
     * Vide puis rempli la liste des ingrédients disponibles à partir de la BD(Pour l'initialiser, puis la rafraîchir)
     */
    void remplirListeIngredients()
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

    void rafraichirListeIngredients()
    {
        SimpleAdapter simpleAdapter=new SimpleAdapter(this,arrayListIng,R.layout.custom_list_ing,from,toIng);
        SimpleAdapter.ViewBinder binder = new SimpleAdapter.ViewBinder() {
            @Override
            public boolean setViewValue(View view, Object object, String value) {

                if (view.equals((TextView) view.findViewById(R.id.nameIng_TXT))) {
                    TextView nomTXT = (TextView) view.findViewById(R.id.nameIng_TXT);
                    if (couleurChoisie.equals("blanc")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if (couleurChoisie.equals("noir")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if (couleurChoisie.equals("jaune")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                if (view.equals((TextView) view.findViewById(R.id.descIng_TXT))) {
                    TextView descTXT = (TextView) view.findViewById(R.id.descIng_TXT);
                    if (couleurChoisie.equals("blanc")) {
                        descTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if (couleurChoisie.equals("noir")) {
                        descTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if (couleurChoisie.equals("jaune")) {
                        descTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                return false;
            }
        };

        simpleAdapter.setViewBinder(binder);
        listeIngredientsLVIEW.setAdapter(simpleAdapter);//sets the adapter for listView
        listeIngredientsLVIEW.setVisibility(View.VISIBLE);
    }

    void rafraichirListeShooters()
    {
        SimpleAdapter simpleAdapter=new SimpleAdapter(this,arrayListIng,R.layout.custom_list_ing,from,toIng);
        SimpleAdapter.ViewBinder binder = new SimpleAdapter.ViewBinder() {
            @Override
            public boolean setViewValue(View view, Object object, String value) {

                if (view.equals((TextView) view.findViewById(R.id.nameIng_TXT))) {
                    TextView nomTXT = (TextView) view.findViewById(R.id.nameIng_TXT);
                    if (couleurChoisie.equals("blanc")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if (couleurChoisie.equals("noir")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if (couleurChoisie.equals("jaune")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                if (view.equals((TextView) view.findViewById(R.id.descIng_TXT))) {
                    TextView descTXT = (TextView) view.findViewById(R.id.descIng_TXT);
                    if (couleurChoisie.equals("blanc")) {
                        descTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if (couleurChoisie.equals("noir")) {
                        descTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if (couleurChoisie.equals("jaune")) {
                        descTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                return false;
            }
        };

        simpleAdapter.setViewBinder(binder);
        listeShootersLVIEW.setAdapter(simpleAdapter);//sets the adapter for listView
        listeShootersLVIEW.setVisibility(View.VISIBLE);
    }

    void rafraichirItemCourant()
    {
        SimpleAdapter simpleAdapter=new SimpleAdapter(this,arrayListItemCourant,R.layout.custom_list_itemcourant,from,toCourant);
        SimpleAdapter.ViewBinder binder = new SimpleAdapter.ViewBinder() {
            @Override
            public boolean setViewValue(View view, Object object, String value) {

                if (view.equals((TextView) view.findViewById(R.id.nameCourant_TXT))) {
                    TextView nomTXT = (TextView) view.findViewById(R.id.nameCourant_TXT);
                    if (couleurChoisie.equals("blanc")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if (couleurChoisie.equals("noir")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if (couleurChoisie.equals("jaune")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                if (view.equals((TextView) view.findViewById(R.id.descCourant_TXT))) {
                    TextView descTXT = (TextView) view.findViewById(R.id.descCourant_TXT);
                    if (couleurChoisie.equals("blanc")) {
                        descTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if (couleurChoisie.equals("noir")) {
                        descTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if (couleurChoisie.equals("jaune")) {
                        descTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                if(view.equals((TextView) view.findViewById(R.id.noteCourant_TXT))) {
                    TextView noteTXT = (TextView) view.findViewById(R.id.noteCourant_TXT);
                    if (couleurChoisie.equals("blanc")) {
                        noteTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if (couleurChoisie.equals("noir")) {
                        noteTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if (couleurChoisie.equals("jaune")) {
                        noteTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                return false;
            }
        };
        simpleAdapter.setViewBinder(binder);

        drinkItemLVIEW.setAdapter(simpleAdapter);//sets the adapter for listView
        drinkItemLVIEW.setVisibility(View.VISIBLE);
    }

    /**
     * Permet d'initialiser et rafraîchir la liste du panier
     */
    void remplirListePanier()
    {
        final ImageButton commandBTN=findViewById(R.id.command_IMGBTN);
        if(arrayListPanier.size()!=0)
            commandBTN.setVisibility(View.VISIBLE);
        else
            commandBTN.setVisibility(View.INVISIBLE);

        SimpleAdapter simpleAdapter=new SimpleAdapter(this, arrayListPanier,R.layout.custom_list_ing,from,toIng);

        SimpleAdapter.ViewBinder binder = new SimpleAdapter.ViewBinder() {
            @Override
            public boolean setViewValue(View view, Object object, String value) {

                if (view.equals((TextView) view.findViewById(R.id.nameIng_TXT))) {
                    TextView nomTXT = (TextView) view.findViewById(R.id.nameIng_TXT);
                    if (couleurChoisie.equals("blanc")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if (couleurChoisie.equals("noir")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if (couleurChoisie.equals("jaune")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                if (view.equals((TextView) view.findViewById(R.id.descIng_TXT))) {
                    TextView descTXT = (TextView) view.findViewById(R.id.descIng_TXT);
                    if (couleurChoisie.equals("blanc")) {
                        descTXT.setTextColor(getResources().getColor(R.color.darkgrey));
                    } else if (couleurChoisie.equals("noir")) {
                        descTXT.setTextColor(getResources().getColor(R.color.white));
                    } else if (couleurChoisie.equals("jaune")) {
                        descTXT.setTextColor(getResources().getColor(R.color.black));
                    }
                }
                return false;
            }
        };
        simpleAdapter.setViewBinder(binder);

        panierLVIEW.setAdapter(simpleAdapter);//sets the adapter for listView
        panierLVIEW.setVisibility(View.VISIBLE);
    }

    @Deprecated
    void selectFirstCartItem()
    {
        if(arrayListPanier.size()>0)
        {
            int defaultPosition = 0;
            int justIgnoreId = 0;
            panierLVIEW.setItemChecked(defaultPosition, true);
            panierLVIEW.performItemClick(panierLVIEW.getSelectedView(),defaultPosition, justIgnoreId);
        }
    }

    //endregion

    //region Notes

    void demanderNote(String nomMix)
    {
        final TextView nomMixTXT=findViewById(R.id.nomMix_TXT);

        nomMixTXT.setText(nomMix);
        notesLYT.setVisibility(View.VISIBLE);
        remplirListePanier();
    }

    void annulerNote() {

        final TextView nomMixTXT=findViewById(R.id.nomMix_TXT);
        nomMixTXT.setText("");

        reinitTableauNotes();
        arrayListPanier.remove(0);
        if(arrayListPanier.size()!=0) {
            demanderNote(arrayListPanier.get(0).get("nom"));
        }
        else
            remplirListePanier();
        afficherNombreItemsPanier();
    }

    void envoyerNote() {

        if(note==0)
            faireToast("Désolé de votre mauvaise expérience. Revenez nous voir.");
        else if(note==1)
            faireToast("Merci d'avoir noté: "+note+" étoile");
        else
            faireToast("Merci d'avoir noté: "+note+" étoiles");

        //ENVOYER ICI A LA BD arrayListPanier.get(0).get("nom") et note
        try {
            String sql = "select Coderecette,nomrecette from recette where nomrecette = '" + arrayListPanier.get(0).get("nom") +"'";
            Statement stm12 = conn_.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_READ_ONLY);
            ResultSet resultSet = stm12.executeQuery(sql);
            resultSet.next();
            Statement statement = conn_.createStatement();
            int codeRecette=resultSet.getInt(1);
            String nomRecette=resultSet.getString(2);
            statement.executeUpdate("INSERT INTO Note VALUES ('" +codeRecette+"','"+ nomRecette+"',"+note+")");
        } catch (SQLException e) {
            e.printStackTrace();
        }

        remplirListeDrinks();
        rafraichirListeDrinks();
        reinitTableauNotes();
        arrayListPanier.remove(0);
        if(arrayListPanier.size()!=0) {
            demanderNote(arrayListPanier.get(0).get("nom"));
        }
        else
            remplirListePanier();
        afficherNombreItemsPanier();
    }

    void reinitTableauNotes() {
        final ImageButton etoile1 = findViewById(R.id.star1_IMGBTN);
        final ImageButton etoile2 = findViewById(R.id.star2_IMGBTN);
        final ImageButton etoile3 = findViewById(R.id.star3_IMGBTN);
        final ImageButton etoile4 = findViewById(R.id.star4_IMGBTN);
        final ImageButton etoile5 = findViewById(R.id.star5_IMGBTN);
        etoile1.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
        etoile2.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
        etoile3.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
        etoile4.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
        etoile5.setImageDrawable(getResources().getDrawable(R.drawable.star_inactive));
        notesLYT.setVisibility(View.INVISIBLE);
        note = 0;
    }

    //endregion

    //region Tri

    void trierHaut()
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

        rafraichirListeDrinks();

        Collections.sort(arrayListIng, new Comparator<HashMap<String,String>>()
        {
            public int compare(HashMap<String,String> o1,
                               HashMap<String,String> o2)
            {
                return o1.get("nom").compareTo(o2.get("nom"));
            }
        });
        rafraichirListeShooters();
    }

    void trierBas()
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
        rafraichirListeDrinks();

        Collections.sort(arrayListIng, new Comparator<HashMap<String,String>>()
        {
            public int compare(HashMap<String,String> o1,
                               HashMap<String,String> o2)
            {
                return -o1.get("nom").compareTo(o2.get("nom"));
            }
        });
        rafraichirListeShooters();
    }

    void enleverTri()
    {
        final TextView triNoteBTN=findViewById(R.id.triNote_BTN);
        triNoteBTN.setText("A-B");
        Collections.sort(arrayListDrink, new Comparator<HashMap<String,String>>()
        {
            public int compare(HashMap<String,String> o1,
                               HashMap<String,String> o2)
            {
                if(!o1.containsValue(null)&&!o2.containsValue(null)) return o1.get("nom").compareTo(o2.get("nom"));
                return 0;
            }
        });
        rafraichirListeDrinks();

        Collections.sort(arrayListIng, new Comparator<HashMap<String,String>>()
        {
            public int compare(HashMap<String,String> o1,
                               HashMap<String,String> o2)
            {
                if(!o1.containsValue(null)&&!o2.containsValue(null)) return o1.get("nom").compareTo(o2.get("nom"));
                return 0;
            }
        });
        rafraichirListeShooters();
    }

    //endregion

    //region Description

    HashMap<String, Integer> defaireDescription(String description)
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

    //endregion

    //region Utilitaires

    void faireToast(String message)
    {
        Toast toast = Toast.makeText(getApplicationContext(),
                message, Toast.LENGTH_LONG);
        toast.setGravity(Gravity.CENTER, 0, hauteur_toast);
        View view = toast.getView();

        view.setBackgroundColor(getResources().getColor(R.color.yellow));
        toast.show();
    }

    String arrondir(float nombre)
    {
        DecimalFormat df = new DecimalFormat("#.##");
        df.setRoundingMode(RoundingMode.DOWN);
        return df.format(nombre);
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

    //endregion

    //region Couleurs

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

        int[] boutons = new int[]{
                getResources().getColor(R.color.white)
                , getResources().getColor(R.color.black)};

        findViewById(R.id.background_LYT).setBackgroundColor(getResources().getColor(R.color.white));
        findViewById(R.id.connexion_LYT).setBackgroundColor(getResources().getColor(R.color.white));
        findViewById(R.id.backgroundFooter_TView).setBackgroundColor(getResources().getColor(R.color.black));

        changerCouleurBoutonsMenu(couleurs.get("blanc"));
        changeTextColor(couleurs.get("noir"));
        changeRadioButtonColor(colorRBTN);
        changerCouleurDrinkLayout(boutons);
        changerCouleurPanierLayout(boutons);
    }

    void changerNoir()
    {
        ColorStateList colorRBTN = new ColorStateList(
                new int[][]{
                        new int[]{-android.R.attr.state_checked},
                        new int[]{android.R.attr.state_checked}
                },
                new int[]{

                        getResources().getColor(R.color.grey)
                        , getResources().getColor(R.color.white)
                }
        );

        int[] boutons = new int[]{
                getResources().getColor(R.color.black)
                , getResources().getColor(R.color.white)};

        findViewById(R.id.background_LYT).setBackgroundColor(getResources().getColor(R.color.black));
        findViewById(R.id.connexion_LYT).setBackgroundColor(getResources().getColor(R.color.black));
        findViewById(R.id.backgroundFooter_TView).setBackgroundColor(getResources().getColor(R.color.white));

        changerCouleurBoutonsMenu(couleurs.get("noir"));
        changeTextColor(couleurs.get("blanc"));
        changeRadioButtonColor(colorRBTN);
        changerCouleurDrinkLayout(boutons);
        changerCouleurPanierLayout(boutons);
    }

    void changerJELLY()
    {
        ColorStateList colorRBTN = new ColorStateList(
                new int[][]{
                        new int[]{-android.R.attr.state_checked},
                        new int[]{android.R.attr.state_checked}
                },
                new int[]{

                        getResources().getColor(R.color.darkgrey)
                        , getResources().getColor(R.color.black)
                }
        );

        int[] boutons = new int[]{
                        getResources().getColor(R.color.yellow)
                        , getResources().getColor(R.color.black)};

        findViewById(R.id.background_LYT).setBackgroundColor(getResources().getColor(R.color.yellow));
        findViewById(R.id.connexion_LYT).setBackgroundColor(getResources().getColor(R.color.yellow));
        findViewById(R.id.backgroundFooter_TView).setBackgroundColor(getResources().getColor(R.color.black));

        changerCouleurBoutonsMenu(couleurs.get("jaune"));
        changeTextColor(couleurs.get("blanc"));
        changeRadioButtonColor(colorRBTN);
        changerCouleurDrinkLayout(boutons);
        changerCouleurPanierLayout(boutons);
    }

    void changerCouleurBoutonsMenu(ColorStateList color)
    {
        drinkBTN.setBackgroundTintList(color);
        panierBTN.setBackgroundTintList(color);
        optionsBTN.setBackgroundTintList(color);
        infosBTN.setBackgroundTintList(color);
    }

    void changeTextColor(ColorStateList color)
    {
        RadioButton noirRBTN = findViewById(R.id.changerNoir_RBTN);
        RadioButton blancRBTN = findViewById(R.id.changerBlanc_RBTN);
        RadioButton jellyRBTN = findViewById(R.id.changerJelly_RBTN);

        TextView optionsTXT=findViewById(R.id.options_TXT);
        TextView infosTXT=findViewById(R.id.infos_TXT);
        TextView texteInfosTXT=findViewById(R.id.informations_TXT);
        TextView connexionTXT=findViewById(R.id.connexion_TXT);
        TextView drinkTXT=findViewById(R.id.drink_TXT);
        TextView shooterTXT=findViewById(R.id.shooter_TXT);
        TextView panierTXT=findViewById(R.id.cart_TXT);

        blancRBTN.setTextColor(color);
        noirRBTN.setTextColor(color);
        jellyRBTN.setTextColor(color);

        optionsTXT.setTextColor(color);
        infosTXT.setTextColor(color);
        texteInfosTXT.setTextColor(color);
        connexionTXT.setTextColor(color);
        drinkTXT.setTextColor(color);
        shooterTXT.setTextColor(color);
        panierTXT.setTextColor(color);
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

    void changerCouleurDrinkLayout(int[] color)
    {

        Button triBTN=findViewById(R.id.triNote_BTN);
        triBTN.setBackgroundColor(color[0]);
        triBTN.setTextColor(color[1]);
    }

    void changerCouleurPanierLayout(int[] color)
    {

        Button supprimerToutBTN=findViewById(R.id.trashall_BTN);
        supprimerToutBTN.setBackgroundColor(color[0]);
        supprimerToutBTN.setTextColor(color[1]);
    }

    //endregion
}