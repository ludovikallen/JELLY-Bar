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
import android.widget.ImageView;
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
    ConstraintLayout drinkLYT, modifierLYT, shooterLYT, panierLYT,infosLYT,notesLYT,connexionLYT;

    /**
     * Variables pour contenir les boutons pour pouvoir changer d'onglet dans l'application
     */
    Button drinkBTN, panierBTN, shooterBTN, infosBTN;

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
     * Couleur du toast selon la couleur choisie
     */
    int couleurToast=R.color.jaune;

    /**
     * Index dans la liste de l'article en cours de modification
     */
    int indexItemModification=-1;

    /**
     * Note présentement choisie pour le drink courant
     */
    Integer note=0;

    /**
     * Easter egg
     */
    String Alcoolique="";

    /**
     * Fonction lancée à la création de l'activité
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        StrictMode.setThreadPolicy(new StrictMode.ThreadPolicy.Builder()
                .detectDiskReads()
                .detectDiskWrites()
                .detectNetwork()   // or .detectAll() for all detectable problems
                .penaltyLog()
                .build());
        StrictMode.setVmPolicy(new StrictMode.VmPolicy.Builder()
                .detectLeakedSqlLiteObjects()
                .detectLeakedClosableObjects()
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
                    runOnUiThread(new Runnable() {
                        public void run() {
                            Toast.makeText(MainActivity.this, "Driver Pour Oracle non disponible", Toast.LENGTH_LONG).show();
                            try {
                                Thread.sleep(6000);
                                finish();
                            } catch (InterruptedException e1) {
                                e1.printStackTrace();
                            }
                        }
                    });
                }
                String jdbcURL = "jdbc:oracle:thin:@mercure.clg.qc.ca:1521:ORCL";
                String user = "barman";
                String passwd = "projet";
                try
                {
                    conn_ = DriverManager.getConnection(jdbcURL,user,passwd);


                }
                catch (java.sql.SQLException se)
                {
                    runOnUiThread(new Runnable() {
                        public void run() {
                            Toast.makeText(MainActivity.this, "Connection a la base de donner impossible", Toast.LENGTH_LONG).show();
                            try {
                                Thread.sleep(6000);
                                finish();
                            } catch (InterruptedException e1) {
                                e1.printStackTrace();
                            }
                        }
                    });
                }
                Thread thread = new Thread(){
                    public void run(){
                        Initialiser();
                    }
                };
                runOnUiThread(thread);
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
        drinkLYT =findViewById(R.id.listeDrink_LYT);
        modifierLYT =findViewById(R.id.listeModifier_LYT);
        panierLYT =findViewById(R.id.panier_LYT);
        shooterLYT=findViewById(R.id.listeShooter_LYT);
        infosLYT=findViewById(R.id.infos_LYT);
        notesLYT=findViewById(R.id.notes_LYT);
        connexionLYT=findViewById(R.id.connexion_LYT);

        drinkBTN=findViewById(R.id.drinks_BTN);
        infosBTN =findViewById(R.id.infos_BTN);
        panierBTN =findViewById(R.id.panier_BTN);
        shooterBTN =findViewById(R.id.shooter_BTN);

        listeDrinksLVIEW =findViewById(R.id.drink_LVIEW);
        listeShootersLVIEW=findViewById(R.id.shooter_LVIEW);
        listeIngredientsLVIEW =findViewById(R.id.ingredients_LVIEW);
        panierLVIEW =findViewById(R.id.panier_LVIEW);
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

        rafraichirListeDrinks();
        rafraichirListeIngredients();
        rafraichirListeShooters();
        rafraichirListePanier();
        rafraichirItemCourant();
    }

    void InitialiserCouleurs()
    {
        int[][] etats = new int[][] {
                new int[] { }
        };
        int[] couleurs = new int[] {
                getResources().getColor(R.color.jaune),
        };

        ColorStateList jaune = new ColorStateList(etats, couleurs);
        this.couleurs.put("jaune",jaune);

        couleurs = new int[] {
                getResources().getColor(R.color.noir),
        };

        ColorStateList noir = new ColorStateList(etats, couleurs);
        this.couleurs.put("noir",noir);

        couleurs = new int[] {
                getResources().getColor(R.color.blanc),
        };

        ColorStateList blanc = new ColorStateList(etats, couleurs);
        this.couleurs.put("blanc",blanc);

        couleurs = new int[] {
                getResources().getColor(R.color.gris),
        };

        ColorStateList gris = new ColorStateList(etats, couleurs);
        this.couleurs.put("gris",gris);

        couleurs = new int[] {
                getResources().getColor(R.color.grisFonce),
        };

        ColorStateList grisFonce = new ColorStateList(etats, couleurs);
        this.couleurs.put("grisFonce",grisFonce);

        couleurs = new int[] {
                getResources().getColor(R.color.bleu),
        };

        ColorStateList bleu = new ColorStateList(etats, couleurs);
        this.couleurs.put("bleu",bleu);
    }

    //endregion

    //region setListeners

    /**
     * Initialise les touch listeners, pour effectuer des actions avant le relâchement du toucher
     */
    @SuppressLint("ClickableViewAccessibility")
    void setTouchListeners()
    {
        final TextView quitterNotesBTN=findViewById(R.id.quitterNotes_BTN);

        final ImageButton etoile1= findViewById(R.id.etoile1_IMGBTN);
        final ImageButton etoile2= findViewById(R.id.etoile2_IMGBTN);
        final ImageButton etoile3= findViewById(R.id.etoile3_IMGBTN);
        final ImageButton etoile4= findViewById(R.id.etoile4_IMGBTN);
        final ImageButton etoile5= findViewById(R.id.etoile5_IMGBTN);

        drinkBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                    drinkBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.gris)));
                    drinkBTN.setBackgroundResource(R.drawable.icondrink);
                return false;
            }
        });

        infosBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                infosBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.gris)));
                infosBTN.setBackgroundResource(R.drawable.iconinfo);
                return false;
            }
        });

        panierBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                panierBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.gris)));
                panierBTN.setBackgroundResource(R.drawable.iconcart);
                return false;
            }
        });

        shooterBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                shooterBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.gris)));
                shooterBTN.setBackgroundResource(R.drawable.iconshooter);
                return false;
            }
        });

        quitterNotesBTN.setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                quitterNotesBTN.setBackgroundColor(getResources().getColor(R.color.grisFonce));
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
        final ImageButton supprimerBTN=findViewById(R.id.supprimer_IMGBTN);
        final TextView supprimerToutBTN=findViewById(R.id.supprimerTout_BTN);
        final ImageButton commanderBTN=findViewById(R.id.commander_IMGBTN);
        final TextView quitterNotesBTN=findViewById(R.id.quitterNotes_BTN);
        final TextView envoyerNoteBTN=findViewById(R.id.envoyerNote_BTN);
        final TextView triNotesBTN=findViewById(R.id.triNote_BTN);
        final TextView triNomBTN=findViewById(R.id.triNom_BTN);
        final Button accepterChangementsBTN=findViewById(R.id.accepterModification_BTN);
        final Button annulerChangementsBTN=findViewById(R.id.annulerModification_BTN);
        final Button connecterBTN= findViewById(R.id.connexion_BTN);

        final ImageButton etoile1= findViewById(R.id.etoile1_IMGBTN);
        final ImageButton etoile2= findViewById(R.id.etoile2_IMGBTN);
        final ImageButton etoile3= findViewById(R.id.etoile3_IMGBTN);
        final ImageButton etoile4= findViewById(R.id.etoile4_IMGBTN);
        final ImageButton etoile5= findViewById(R.id.etoile5_IMGBTN);

        final ImageView logoIMG=findViewById(R.id.logo1_IMG);

        logoIMG.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                drinkLYT.setVisibility(View.INVISIBLE);
                shooterLYT.setVisibility(View.INVISIBLE);
                modifierLYT.setVisibility(View.INVISIBLE);
                shooterLYT.setVisibility(View.INVISIBLE);
                panierLYT.setVisibility(View.INVISIBLE);
                infosLYT.setVisibility(View.INVISIBLE);

                drinkBTN.setVisibility(View.GONE);
                shooterBTN.setVisibility(View.GONE);
                panierBTN.setVisibility(View.GONE);
                infosBTN.setVisibility(View.GONE);

                connexionLYT.setVisibility(View.VISIBLE);
            }
        });

        drinkBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                switch (couleurChoisie) {
                    case "blanc":
                        changerCouleurBoutonsMenu(couleurs.get("blanc"));
                        break;
                    case "noir":
                        changerCouleurBoutonsMenu(couleurs.get("noir"));
                        break;
                    case "jaune":
                        changerCouleurBoutonsMenu(couleurs.get("jaune"));
                        break;
                    case "gris":
                        changerCouleurBoutonsMenu(couleurs.get("grisFonce"));
                        break;
                    case "bleu":
                        changerCouleurBoutonsMenu(couleurs.get("bleu"));
                        break;
                }
                drinkBTN.setBackgroundResource(R.drawable.icondrink);

                drinkLYT.setVisibility(View.VISIBLE);
                shooterLYT.setVisibility(View.INVISIBLE);
                modifierLYT.setVisibility(View.INVISIBLE);
                panierLYT.setVisibility(View.INVISIBLE);
                infosLYT.setVisibility(View.INVISIBLE);

                rafraichirListeDrinks();
                rafraichirListeIngredients();
                rafraichirListeShooters();
                rafraichirItemCourant();
                remplirListeDrinks();
                remplirListeIngredients();
                rafraichirListePanier();
                enleverTri();

                selectionPositionsPanier.clear();
            }
        });

        panierBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                switch (couleurChoisie) {
                    case "blanc":
                        changerCouleurBoutonsMenu(couleurs.get("blanc"));
                        break;
                    case "noir":
                        changerCouleurBoutonsMenu(couleurs.get("noir"));
                        break;
                    case "jaune":
                        changerCouleurBoutonsMenu(couleurs.get("jaune"));
                        break;
                    case "gris":
                        changerCouleurBoutonsMenu(couleurs.get("grisFonce"));
                        break;
                    case "bleu":
                        changerCouleurBoutonsMenu(couleurs.get("bleu"));
                        break;
                }
                panierBTN.setBackgroundResource(R.drawable.iconcart);

                drinkLYT.setVisibility(View.INVISIBLE);
                shooterLYT.setVisibility(View.INVISIBLE);
                modifierLYT.setVisibility(View.INVISIBLE);
                panierLYT.setVisibility(View.VISIBLE);
                infosLYT.setVisibility(View.INVISIBLE);
            }
        });

        shooterBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                switch (couleurChoisie) {
                    case "blanc":
                        changerCouleurBoutonsMenu(couleurs.get("blanc"));
                        break;
                    case "noir":
                        changerCouleurBoutonsMenu(couleurs.get("noir"));
                        break;
                    case "jaune":
                        changerCouleurBoutonsMenu(couleurs.get("jaune"));
                        break;
                    case "gris":
                        changerCouleurBoutonsMenu(couleurs.get("grisFonce"));
                        break;
                    case "bleu":
                        changerCouleurBoutonsMenu(couleurs.get("bleu"));
                        break;
                }
                shooterBTN.setBackgroundResource(R.drawable.iconshooter);

                drinkLYT.setVisibility(View.INVISIBLE);
                shooterLYT.setVisibility(View.VISIBLE);
                modifierLYT.setVisibility(View.INVISIBLE);
                panierLYT.setVisibility(View.INVISIBLE);
                infosLYT.setVisibility(View.INVISIBLE);

                trierBas();
                selectionPositionsPanier.clear();
            }
        });

        infosBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                switch (couleurChoisie) {
                    case "blanc":
                        changerCouleurBoutonsMenu(couleurs.get("blanc"));
                        break;
                    case "noir":
                        changerCouleurBoutonsMenu(couleurs.get("noir"));
                        break;
                    case "jaune":
                        changerCouleurBoutonsMenu(couleurs.get("jaune"));
                        break;
                    case "gris":
                        changerCouleurBoutonsMenu(couleurs.get("grisFonce"));
                        break;
                    case "bleu":
                        changerCouleurBoutonsMenu(couleurs.get("bleu"));
                        break;
                }
                infosBTN.setBackgroundResource(R.drawable.iconinfo);

                drinkLYT.setVisibility(View.INVISIBLE);
                shooterLYT.setVisibility(View.INVISIBLE);
                modifierLYT.setVisibility(View.INVISIBLE);
                panierLYT.setVisibility(View.INVISIBLE);
                infosLYT.setVisibility(View.VISIBLE);

                selectionPositionsPanier.clear();
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
                rafraichirListePanier();
                supprimerBTN.setVisibility(View.INVISIBLE);
                afficherNombreItemsPanier();
            }
        });

        commanderBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                commander();
            }
        });

        quitterNotesBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                quitterNotesBTN.setBackgroundColor(getResources().getColor(R.color.gris));
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

        triNomBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if(triNomBTN.getText().equals("▲"))
                {
                    trierBas();
                }
                else if(triNomBTN.getText().equals("▼"))
                {
                    trierHaut();
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
                rafraichirListePanier();
            }
        });

        annulerChangementsBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                indexItemModification=-1;
                arrayListItemCourant.clear();
                modifierLYT.setVisibility(View.INVISIBLE);
                drinkLYT.setVisibility(View.VISIBLE);
            }
        });

        connecterBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                connexionLYT.setVisibility(View.INVISIBLE);
                drinkBTN.setVisibility(View.VISIBLE);
                panierBTN.setVisibility(View.VISIBLE);
                shooterBTN.setVisibility(View.VISIBLE);
                infosBTN.setVisibility(View.VISIBLE);

                TextView label=findViewById(R.id.connexion_TXT);
                label.setText(getResources().getString(R.string.pause_str));
                label.setPaintFlags(label.getPaintFlags() | Paint.UNDERLINE_TEXT_FLAG);

                Alcoolique+="...";
                connecterBTN.setText("Continuer à boire un coup"+Alcoolique);
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

                HashMap<String, String> item = (HashMap<String, String>)adapterView.getItemAtPosition(position);
                faireToast("x1 " + item.values().toArray()[1] + " ajouté au panier");

                ajouterPanier(item);
            }
        });

        listeShootersLVIEW.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int position,
                                    long id) {

                HashMap<String, String> item = (HashMap<String, String>)adapterView.getItemAtPosition(position);
                faireToast("x1 shooter de " + item.values().toArray()[1] + " ajouté au panier");

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
                            else if(couleurChoisie.equals("jaune"))
                                panierLVIEW.getChildAt(position).setBackgroundColor(couleurs.get("gris").getDefaultColor());
                            else if(couleurChoisie.equals("bleu"))
                                panierLVIEW.getChildAt(position).setBackgroundColor(couleurs.get("bleu").getDefaultColor());

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

                    drinkLYT.setVisibility(View.INVISIBLE);
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
                    couleurToast=R.color.jaune;
                }
                else if(checkedId==R.id.changerNoir_RBTN)
                {
                    changerNoir();
                    couleurChoisie="noir";
                    couleurToast=R.color.jaune;
                }
                else if(checkedId==R.id.changerJelly_RBTN)
                {
                    changerJELLY();
                    couleurChoisie="jaune";
                    couleurToast=R.color.gris;
                }
                else if(checkedId==R.id.changerGris_RBTN)
                {
                    changerGris();
                    couleurChoisie="gris";
                    couleurToast=R.color.gris;
                }
                else if(checkedId==R.id.changerBleu_RBTN)
                {
                    changerBleu();
                    couleurChoisie="bleu";
                    couleurToast=R.color.grisFonce;
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
                rafraichirListePanier();
                arrayListPanier.clear();
                afficherNombreItemsPanier();

                final ImageButton commandBTN=findViewById(R.id.commander_IMGBTN);
                commandBTN.setVisibility(View.INVISIBLE);
                faireToast("Notes annulées.");
                final TextView panierTXT=findViewById(R.id.panier_TXT);
                panierTXT.setText(getResources().getString(R.string.panierVide_str));
                panierTXT.setPaintFlags(panierTXT.getPaintFlags() | Paint.UNDERLINE_TEXT_FLAG);
            }
        }
        return super.dispatchTouchEvent(ev);
    }

    //endregion

    //region Panier

    public void commander()
    {
        HashMap<String, Integer> drink;
        String sql2 = "Select max(numcommande) from commande";
        int Numcommande=0;
        ResultSet resultSetMax = null;
        Statement stm12 = null;
        try {
            stm12 = conn_.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_READ_ONLY);
            resultSetMax = stm12.executeQuery(sql2);
            resultSetMax.next();
            Numcommande = resultSetMax.getInt(1) + 1;

        } catch (SQLException e) {
            e.printStackTrace();
        }
        try {
            if (resultSetMax != null ) {
                resultSetMax.close();
                stm12.close();
            }

        } catch (SQLException e) {
            e.printStackTrace();
        }
        Statement stm1 = null;
        ResultSet resultSet = null;
        for (int i = 0; i < arrayListPanier.size(); i++)
        {
            drink= defaireDescription(arrayListPanier.get(i).get("desc"));
            for ( String key : drink.keySet() ) {
                Object value = drink.get(key);
                String sql = "select codebouteille from INGREDIENT where nombouteille = '" + key +"'";
                try {
                    String Nomrecette = arrayListPanier.get(i).get("nom");
                    stm1 = conn_.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_READ_ONLY);
                    resultSet = stm1.executeQuery(sql);
                    resultSet.next();
                    Statement statement = conn_.createStatement();
                    int coderecette = resultSet.getInt(1);
                    String SQL = "INSERT INTO COMMANDE VALUES ( "+  (Numcommande + i) + ","+ coderecette +","+ value +",'"+ Nomrecette+ "')";
                    statement.executeUpdate(SQL);
                } catch (SQLException e) {
                    e.printStackTrace();

                }
            }
        }
        try {
            if (stm1 != null && resultSet != null)
            {
                stm1.close();
                resultSet.close();
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        demanderNote(arrayListPanier.get(0).get("nom"));
        faireToast("Merci de votre commande. Veuillez noter s'il vous plait.");
        selectionPositionsPanier.clear();
        rafraichirListePanier();
        remplirListeDrinks();
    }

    /**
     * Ajoute l'élément envoyé en paramètre dans le panier
     */
    void ajouterPanier(HashMap<String, String> ajout)
    {
        arrayListPanier.add(ajout);
        rafraichirListePanier();
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
            rafraichirListePanier();
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
        rafraichirListePanier();
        if(compteurItems==1)
            faireToast(compteurItems + " item retiré du panier");
        else
            faireToast(compteurItems + " items retirés du panier");
        afficherNombreItemsPanier();
    }

    void afficherNombreItemsPanier()
    {
        final TextView itemCountTXT=findViewById(R.id.nombreArticlesPanier_TXT);
        itemCountTXT.setText(Integer.toString(arrayListPanier.size()));
        final TextView panierTXT=findViewById(R.id.panier_TXT);
        if(arrayListPanier.size()==0)

            panierTXT.setText(getResources().getString(R.string.panierVide_str));
        else {
            panierTXT.setText(getResources().getString(R.string.panier_str));
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
        Statement stm1 =null;
        PreparedStatement stmlNote=null;
        ResultSet resultSet=null;
        ResultSet resultSetNote=null;
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
        try {
            if (stm1 != null && stmlNote != null && resultSet != null &&resultSetNote != null) {
                stm1.close();
                stmlNote.close();
                resultSet.close();
                resultSetNote.close();
            }

        } catch (SQLException e) {
            e.printStackTrace();
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
                        nomTXT.setTextColor(getResources().getColor(R.color.grisFonce));
                    } else if(couleurChoisie.equals("noir")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.blanc));
                    } else if(couleurChoisie.equals("jaune")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("gris")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("bleu")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.blanc));
                    }
                }
                if (view.equals((TextView) view.findViewById(R.id.descDrink_TXT))) {
                    TextView descTXT = (TextView) view.findViewById(R.id.descDrink_TXT);
                    if (couleurChoisie.equals("blanc")) {
                        descTXT.setTextColor(getResources().getColor(R.color.grisFonce));
                    } else if (couleurChoisie.equals("noir")) {
                        descTXT.setTextColor(getResources().getColor(R.color.blanc));
                    } else if (couleurChoisie.equals("jaune")) {
                        descTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("gris")) {
                        descTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("bleu")) {
                        descTXT.setTextColor(getResources().getColor(R.color.blanc));
                    }
                }
                if(view.equals((TextView) view.findViewById(R.id.noteDrink_TXT))) {
                    TextView noteTXT = (TextView) view.findViewById(R.id.noteDrink_TXT);
                    if (couleurChoisie.equals("blanc")) {
                        noteTXT.setTextColor(getResources().getColor(R.color.grisFonce));
                    } else if (couleurChoisie.equals("noir")) {
                        noteTXT.setTextColor(getResources().getColor(R.color.blanc));
                    } else if (couleurChoisie.equals("jaune")) {
                        noteTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("gris")) {
                        noteTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("bleu")) {
                        noteTXT.setTextColor(getResources().getColor(R.color.blanc));
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
        Statement stm1 = null;
        ResultSet resultSet = null;
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
        }finally {
            try {
                if (stm1 != null && resultSet != null) {
                    resultSet.close();
                    stm1.close();
                }
            }catch (SQLException e){  e.printStackTrace();};
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
                        nomTXT.setTextColor(getResources().getColor(R.color.grisFonce));
                    } else if (couleurChoisie.equals("noir")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.blanc));
                    } else if (couleurChoisie.equals("jaune")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("gris")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("bleu")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.blanc));
                    }
                }
                if (view.equals((TextView) view.findViewById(R.id.descIng_TXT))) {
                    TextView descTXT = (TextView) view.findViewById(R.id.descIng_TXT);
                    if (couleurChoisie.equals("blanc")) {
                        descTXT.setTextColor(getResources().getColor(R.color.grisFonce));
                    } else if (couleurChoisie.equals("noir")) {
                        descTXT.setTextColor(getResources().getColor(R.color.blanc));
                    } else if (couleurChoisie.equals("jaune")) {
                        descTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("gris")) {
                        descTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("bleu")) {
                        descTXT.setTextColor(getResources().getColor(R.color.blanc));
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
                        nomTXT.setTextColor(getResources().getColor(R.color.grisFonce));
                    } else if (couleurChoisie.equals("noir")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.blanc));
                    } else if (couleurChoisie.equals("jaune")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("gris")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("bleu")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.blanc));
                    }
                }
                if (view.equals((TextView) view.findViewById(R.id.descIng_TXT))) {
                    TextView descTXT = (TextView) view.findViewById(R.id.descIng_TXT);
                    if (couleurChoisie.equals("blanc")) {
                        descTXT.setTextColor(getResources().getColor(R.color.grisFonce));
                    } else if (couleurChoisie.equals("noir")) {
                        descTXT.setTextColor(getResources().getColor(R.color.blanc));
                    } else if (couleurChoisie.equals("jaune")) {
                        descTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("gris")) {
                        descTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("bleu")) {
                        descTXT.setTextColor(getResources().getColor(R.color.blanc));
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
                        nomTXT.setTextColor(getResources().getColor(R.color.grisFonce));
                    } else if (couleurChoisie.equals("noir")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.blanc));
                    } else if (couleurChoisie.equals("jaune")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("gris")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("bleu")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.blanc));
                    }
                }
                if (view.equals((TextView) view.findViewById(R.id.descCourant_TXT))) {
                    TextView descTXT = (TextView) view.findViewById(R.id.descCourant_TXT);
                    if (couleurChoisie.equals("blanc")) {
                        descTXT.setTextColor(getResources().getColor(R.color.grisFonce));
                    } else if (couleurChoisie.equals("noir")) {
                        descTXT.setTextColor(getResources().getColor(R.color.blanc));
                    } else if (couleurChoisie.equals("jaune")) {
                        descTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("gris")) {
                        descTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("bleu")) {
                        descTXT.setTextColor(getResources().getColor(R.color.blanc));
                    }
                }
                if(view.equals((TextView) view.findViewById(R.id.noteCourant_TXT))) {
                    TextView noteTXT = (TextView) view.findViewById(R.id.noteCourant_TXT);
                    if (couleurChoisie.equals("blanc")) {
                        noteTXT.setTextColor(getResources().getColor(R.color.grisFonce));
                    } else if (couleurChoisie.equals("noir")) {
                        noteTXT.setTextColor(getResources().getColor(R.color.blanc));
                    } else if (couleurChoisie.equals("jaune")) {
                        noteTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("gris")) {
                        noteTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("bleu")) {
                        noteTXT.setTextColor(getResources().getColor(R.color.blanc));
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
    void rafraichirListePanier()
    {
        final ImageButton commandBTN=findViewById(R.id.commander_IMGBTN);
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
                        nomTXT.setTextColor(getResources().getColor(R.color.grisFonce));
                    } else if (couleurChoisie.equals("noir")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.blanc));
                    } else if (couleurChoisie.equals("jaune")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("gris")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("bleu")) {
                        nomTXT.setTextColor(getResources().getColor(R.color.blanc));
                    }
                }
                if (view.equals((TextView) view.findViewById(R.id.descIng_TXT))) {
                    TextView descTXT = (TextView) view.findViewById(R.id.descIng_TXT);
                    if (couleurChoisie.equals("blanc")) {
                        descTXT.setTextColor(getResources().getColor(R.color.grisFonce));
                    } else if (couleurChoisie.equals("noir")) {
                        descTXT.setTextColor(getResources().getColor(R.color.blanc));
                    } else if (couleurChoisie.equals("jaune")) {
                        descTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("gris")) {
                        descTXT.setTextColor(getResources().getColor(R.color.noir));
                    } else if(couleurChoisie.equals("bleu")) {
                        descTXT.setTextColor(getResources().getColor(R.color.blanc));
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
        rafraichirListePanier();
    }

    void annulerNote() {

        final TextView nomMixTXT=findViewById(R.id.nomMix_TXT);
        nomMixTXT.setText("");

        reinitTableauNotes();
        if(arrayListPanier.size()!=0)
            arrayListPanier.remove(0);
        if(arrayListPanier.size()!=0) {
            demanderNote(arrayListPanier.get(0).get("nom"));
        }
        else
            rafraichirListePanier();
        afficherNombreItemsPanier();
    }

    void envoyerNote() {

        if(note==0)
            faireToast("Désolé de votre mauvaise expérience. Revenez nous voir.");
        else if(note==1)
            faireToast("Merci d'avoir noté: "+note+" étoile");
        else
            faireToast("Merci d'avoir noté: "+note+" étoiles");
        if(arrayListPanier.size()!=0) {
            //ENVOYER ICI A LA BD arrayListPanier.get(0).get("nom") et note
            Statement stm12 = null;
            ResultSet resultSet = null;
            Statement statement = null;
            try {
                String sql = "select Coderecette,nomrecette from recette where nomrecette = '" + arrayListPanier.get(0).get("nom") + "'";
                stm12 = conn_.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_READ_ONLY);
                resultSet = stm12.executeQuery(sql);
                resultSet.next();
                statement = conn_.createStatement();
                int codeRecette = resultSet.getInt(1);
                String nomRecette = resultSet.getString(2);
                statement.executeUpdate("INSERT INTO Note VALUES ('" + codeRecette + "','" + nomRecette + "'," + note + ")");
            } catch (SQLException e) {
                e.printStackTrace();
            } finally {
                try {
                    if (stm12 != null && resultSet != null && statement != null) {
                        resultSet.close();
                        stm12.close();
                        statement.close();
                    }
                } catch (SQLException e) {
                    e.printStackTrace();
                }
                ;
            }
            arrayListPanier.remove(0);
        }
        remplirListeDrinks();
        rafraichirListeDrinks();
        reinitTableauNotes();
        if(arrayListPanier.size()!=0) {
            demanderNote(arrayListPanier.get(0).get("nom"));
        }
        else
            rafraichirListePanier();
        afficherNombreItemsPanier();
    }

    void reinitTableauNotes() {
        final ImageButton etoile1 = findViewById(R.id.etoile1_IMGBTN);
        final ImageButton etoile2 = findViewById(R.id.etoile2_IMGBTN);
        final ImageButton etoile3 = findViewById(R.id.etoile3_IMGBTN);
        final ImageButton etoile4 = findViewById(R.id.etoile4_IMGBTN);
        final ImageButton etoile5 = findViewById(R.id.etoile5_IMGBTN);
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
        final TextView triNomBTN=findViewById(R.id.triNom_BTN);
        triNoteBTN.setText("▲");
        triNomBTN.setText("▲");
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
                return -o1.get("nom").compareTo(o2.get("nom"));
            }
        });
        rafraichirListeShooters();
    }

    void trierBas()
    {
        final TextView triNoteBTN=findViewById(R.id.triNote_BTN);
        final TextView triNomBTN=findViewById(R.id.triNom_BTN);
        triNoteBTN.setText("▼");
        triNomBTN.setText("▼");
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
                return o1.get("nom").compareTo(o2.get("nom"));
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
        if(description.contains("oz")) {
            while (line.contains(",")) {
                tableauIngredients.add(line.substring(0, line.indexOf(",")));
                line = line.substring(line.indexOf(",") + 1);
            }
            tableauIngredients.add(line);

            for (int i = 0; i < tableauIngredients.size(); i++) {
                String ElementCommande[] = tableauIngredients.get(i).split("oz");
                ingredients.put(ElementCommande[1].trim(), Integer.valueOf(ElementCommande[0].trim()));
            }
        }
        else {
            for (int i = 0; i < arrayListIng.size(); i++) {
                if (arrayListIng.get(i).containsValue(description)) {
                    ingredients.put(arrayListIng.get(i).get("nom"), 1);
                    break;
                }
            }
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

        view.setBackgroundColor(getResources().getColor(couleurToast));
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
        Statement stm1s = null;
        ResultSet setRecette = null;

        String requeteNombreRecette = "select count(*) from recette";
        try {
            stm1s = conn_.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE,ResultSet.CONCUR_READ_ONLY);
            setRecette = stm1s.executeQuery(requeteNombreRecette);
            setRecette.next();
            return setRecette.getInt(1);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        try {
            if (stm1s != null && setRecette != null ) {
                setRecette.close();
                stm1s.close();

            }
        }catch (SQLException e){  e.printStackTrace();}
        return -1;
    }

    //endregion

    //region Couleurs

    void changerCouleurBoutonsMenu(ColorStateList color)
    {
        drinkBTN.setBackgroundTintList(color);
        panierBTN.setBackgroundTintList(color);
        shooterBTN.setBackgroundTintList(color);
        infosBTN.setBackgroundTintList(color);
    }

    void changeTextColor(ColorStateList color)
    {
        TextView optionsTXT=findViewById(R.id.options_TXT);
        TextView infosTXT=findViewById(R.id.infos_TXT);
        TextView texteInfosTXT=findViewById(R.id.informations_TXT);
        TextView connexionTXT=findViewById(R.id.connexion_TXT);
        TextView drinkTXT=findViewById(R.id.drink_TXT);
        TextView shooterTXT=findViewById(R.id.shooter_TXT);
        TextView panierTXT=findViewById(R.id.panier_TXT);

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
        RadioButton grisRBTN = findViewById(R.id.changerGris_RBTN);
        RadioButton bleuRBTN = findViewById(R.id.changerBleu_RBTN);

        blancRBTN.setButtonTintList(color);
        noirRBTN.setButtonTintList(color);
        jellyRBTN.setButtonTintList(color);
        grisRBTN.setButtonTintList(color);
        bleuRBTN.setButtonTintList(color);

        blancRBTN.setTextColor(color);
        noirRBTN.setTextColor(color);
        jellyRBTN.setTextColor(color);
        grisRBTN.setTextColor(color);
        bleuRBTN.setTextColor(color);
    }


    void changerCouleurBoutons(int[] color)
    {
        Button triNotesBTN=findViewById(R.id.triNote_BTN);
        triNotesBTN.setBackgroundColor(color[0]);
        triNotesBTN.setTextColor(color[1]);

        Button triNomBTN=findViewById(R.id.triNom_BTN);
        triNomBTN.setBackgroundColor(color[0]);
        triNomBTN.setTextColor(color[1]);

        Button supprimerToutBTN=findViewById(R.id.supprimerTout_BTN);
        supprimerToutBTN.setBackgroundColor(color[0]);
        supprimerToutBTN.setTextColor(color[1]);
    }

    void changerBlanc() {
        ColorStateList colorRBTN = new ColorStateList(
                new int[][]{
                        new int[]{-android.R.attr.state_checked},
                        new int[]{android.R.attr.state_checked}
                },
                new int[]{

                        getResources().getColor(R.color.gris)
                        , getResources().getColor(R.color.jaune)
                }
        );

        int[] boutons = new int[]{
                getResources().getColor(R.color.blanc)
                , getResources().getColor(R.color.noir)};

        Button connexionBTN=findViewById(R.id.connexion_BTN);

        findViewById(R.id.background_LYT).setBackgroundColor(getResources().getColor(R.color.blanc));
        findViewById(R.id.connexion_LYT).setBackgroundColor(getResources().getColor(R.color.blanc));
        connexionBTN.setBackgroundColor(getResources().getColor(R.color.blanc));
        connexionBTN.setTextColor(getResources().getColor(R.color.noir));
        findViewById(R.id.backgroundFooter_TView).setBackgroundColor(getResources().getColor(R.color.noir));

        changerCouleurBoutonsMenu(couleurs.get("blanc"));
        changeTextColor(couleurs.get("noir"));
        changeRadioButtonColor(colorRBTN);
        changerCouleurBoutons(boutons);
    }

    void changerNoir()
    {
        ColorStateList colorRBTN = new ColorStateList(
                new int[][]{
                        new int[]{-android.R.attr.state_checked},
                        new int[]{android.R.attr.state_checked}
                },
                new int[]{

                        getResources().getColor(R.color.gris)
                        , getResources().getColor(R.color.blanc)
                }
        );

        int[] boutons = new int[]{
                getResources().getColor(R.color.noir)
                , getResources().getColor(R.color.blanc)};

        Button connexionBTN=findViewById(R.id.connexion_BTN);

        findViewById(R.id.background_LYT).setBackgroundColor(getResources().getColor(R.color.noir));
        findViewById(R.id.connexion_LYT).setBackgroundColor(getResources().getColor(R.color.noir));
        connexionBTN.setBackgroundColor(getResources().getColor(R.color.noir));
        connexionBTN.setTextColor(getResources().getColor(R.color.blanc));
        findViewById(R.id.backgroundFooter_TView).setBackgroundColor(getResources().getColor(R.color.blanc));

        changerCouleurBoutonsMenu(couleurs.get("noir"));
        changeTextColor(couleurs.get("blanc"));
        changeRadioButtonColor(colorRBTN);
    }

    void changerJELLY() {
        ColorStateList colorRBTN = new ColorStateList(
                new int[][]{
                        new int[]{-android.R.attr.state_checked},
                        new int[]{android.R.attr.state_checked}
                },
                new int[]{

                        getResources().getColor(R.color.grisFonce)
                        , getResources().getColor(R.color.noir)
                }
        );

        int[] boutons = new int[]{
                getResources().getColor(R.color.jaune)
                , getResources().getColor(R.color.noir)};

        Button connexionBTN=findViewById(R.id.connexion_BTN);

        findViewById(R.id.background_LYT).setBackgroundColor(getResources().getColor(R.color.jaune));
        findViewById(R.id.connexion_LYT).setBackgroundColor(getResources().getColor(R.color.jaune));
        connexionBTN.setBackgroundColor(getResources().getColor(R.color.jaune));
        connexionBTN.setTextColor(getResources().getColor(R.color.noir));
        findViewById(R.id.backgroundFooter_TView).setBackgroundColor(getResources().getColor(R.color.noir));

        changerCouleurBoutonsMenu(couleurs.get("jaune"));
        changeTextColor(couleurs.get("blanc"));
        changeRadioButtonColor(colorRBTN);
        changerCouleurBoutons(boutons);
    }

    void changerGris()
    {
        ColorStateList colorRBTN = new ColorStateList(
                new int[][]{
                        new int[]{-android.R.attr.state_checked},
                        new int[]{android.R.attr.state_checked}
                },
                new int[]{

                        getResources().getColor(R.color.blanc)
                        , getResources().getColor(R.color.noir)
                }
        );

        int[] boutons = new int[]{
                getResources().getColor(R.color.blanc)
                , getResources().getColor(R.color.grisFonce)};

        Button connexionBTN=findViewById(R.id.connexion_BTN);

        findViewById(R.id.background_LYT).setBackgroundColor(getResources().getColor(R.color.gris));
        findViewById(R.id.connexion_LYT).setBackgroundColor(getResources().getColor(R.color.gris));
        connexionBTN.setBackgroundColor(getResources().getColor(R.color.gris));
        connexionBTN.setTextColor(getResources().getColor(R.color.noir));
        findViewById(R.id.backgroundFooter_TView).setBackgroundColor(getResources().getColor(R.color.blanc));

        changerCouleurBoutonsMenu(couleurs.get("noir"));
        changeTextColor(couleurs.get("blanc"));
        changeRadioButtonColor(colorRBTN);
        changerCouleurBoutons(boutons);
    }

    void changerBleu()
    {
        ColorStateList colorRBTN = new ColorStateList(
                new int[][]{
                        new int[]{-android.R.attr.state_checked},
                        new int[]{android.R.attr.state_checked}
                },
                new int[]{

                        getResources().getColor(R.color.blanc)
                        , getResources().getColor(R.color.noir)
                }
        );

        int[] boutons = new int[]{
                getResources().getColor(R.color.blanc)
                , getResources().getColor(R.color.noir)};

        Button connexionBTN=findViewById(R.id.connexion_BTN);

        findViewById(R.id.background_LYT).setBackgroundColor(getResources().getColor(R.color.bleu));
        findViewById(R.id.connexion_LYT).setBackgroundColor(getResources().getColor(R.color.bleu));
        connexionBTN.setBackgroundColor(getResources().getColor(R.color.bleu));
        connexionBTN.setTextColor(getResources().getColor(R.color.noir));
        findViewById(R.id.backgroundFooter_TView).setBackgroundColor(getResources().getColor(R.color.blanc));

        changerCouleurBoutonsMenu(couleurs.get("bleu"));
        changeTextColor(couleurs.get("blanc"));
        changeRadioButtonColor(colorRBTN);
        changerCouleurBoutons(boutons);
    }
    //endregion
}