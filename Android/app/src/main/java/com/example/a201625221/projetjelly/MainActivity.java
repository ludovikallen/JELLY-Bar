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
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;
import android.os.StrictMode;

import org.w3c.dom.Text;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import static android.media.CamcorderProfile.get;

public class MainActivity extends AppCompatActivity {
    public static Connection conn_ = null;
    int toast_height=420;
    /**
     * Variables pour contenir les layouts pour pouvoir changer d'onglet dans l'application
     */
    ConstraintLayout listDrinkLYT,listIngLYT,optionsLYT,cartLYT,infosLYT,notesLYT;

    /**
     * Variables pour contenir les boutons pour pouvoir changer d'onglet dans l'application
     */
    Button drinkBTN,cartBTN,optionsBTN,infoBTN;

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
        infoBTN=findViewById(R.id.infos_BTN);
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
       // refreshCartItemCount();
    }

    /**
     * Initialise les touch listeners, pour effectuer des actions avant le relâchement du toucher
     */
    void setTouchListeners()
    {
        final ImageButton trashBTN=findViewById(R.id.trash_IMGBTN);
        final ImageButton commandBTN=findViewById(R.id.command_IMGBTN);
        final TextView noteExitBTN=findViewById(R.id.exitNoteBTN);

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
        final ImageButton etoile1= findViewById(R.id.star1_IMGBTN);
        final ImageButton etoile2= findViewById(R.id.star2_IMGBTN);
        final ImageButton etoile3= findViewById(R.id.star3_IMGBTN);
        final ImageButton etoile4= findViewById(R.id.star4_IMGBTN);
        final ImageButton etoile5= findViewById(R.id.star5_IMGBTN);

        drinkBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                drinkBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.white)));
                drinkBTN.setBackgroundResource(R.drawable.icondrink);

                listDrinkLYT.setVisibility(View.VISIBLE);
                listIngLYT.setVisibility(View.INVISIBLE);
                optionsLYT.setVisibility(View.INVISIBLE);
                cartLYT.setVisibility(View.INVISIBLE);
                infosLYT.setVisibility(View.INVISIBLE);

                fillDrinksList();
            }
        });

        cartBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                cartBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.white)));
                cartBTN.setBackgroundResource(R.drawable.iconcart);

                listDrinkLYT.setVisibility(View.INVISIBLE);
                listIngLYT.setVisibility(View.INVISIBLE);
                optionsLYT.setVisibility(View.INVISIBLE);
                cartLYT.setVisibility(View.VISIBLE);
                infosLYT.setVisibility(View.INVISIBLE);
            }
        });

        optionsBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                optionsBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.white)));
                optionsBTN.setBackgroundResource(R.drawable.iconoptions);

                listDrinkLYT.setVisibility(View.INVISIBLE);
                listIngLYT.setVisibility(View.INVISIBLE);
                optionsLYT.setVisibility(View.VISIBLE);
                cartLYT.setVisibility(View.INVISIBLE);
                infosLYT.setVisibility(View.INVISIBLE);
            }
        });

        infoBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                infoBTN.setBackgroundTintList(ColorStateList.valueOf(getResources().getColor(R.color.white)));
                infoBTN.setBackgroundResource(R.drawable.iconinfo);

                listDrinkLYT.setVisibility(View.INVISIBLE);
                listIngLYT.setVisibility(View.INVISIBLE);
                optionsLYT.setVisibility(View.INVISIBLE);
                cartLYT.setVisibility(View.INVISIBLE);
                infosLYT.setVisibility(View.VISIBLE);
            }
        });

        trashBTN.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                trashBTN.setBackgroundColor(getResources().getColor(R.color.white));
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
                    triNoteBTN.setText("▲");
                    TrierEtoileHaut();
                }
                else if(triNoteBTN.getText().equals("▲"))
                {
                    triNoteBTN.setText("A-B");
                    EnleverTri();
                }
                else if(triNoteBTN.getText().equals("A-B"))
                {
                    triNoteBTN.setText("▼");
                    TrierEtoileBas();
                }
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

                HashMap<String, String> item = ( HashMap<String, String>)adapterView.getItemAtPosition(position);
                faireToast("x1 " + item.values().toArray()[0] + " ajouté au panier");

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
        faireToast("x " + compteurItems + " items retiré du panier");

        refreshCartItemCount();
    }

    /**
     * Vide puis rempli la liste des drinks disponibles à partir de la BD(Pour l'initialiser, puis la rafraîchir)
     */
    void fillDrinksList() {
        arrayListDrink.clear();
        Statement stm1s;
        Statement stm1;
        ResultSet resultSet;
        ResultSet setRecette;
        int nombreRecette = 0;
        String requeteNombreRecette = "select count(*) from recette";
        try {
            stm1s = conn_.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE,ResultSet.CONCUR_READ_ONLY);
            setRecette = stm1s.executeQuery(requeteNombreRecette);
            setRecette.next();
            nombreRecette = setRecette.getInt(1);
        } catch (SQLException e) {
            e.printStackTrace();
        }

        for (int i = 1; i <= nombreRecette; i++)
        {
            try {
                String requeteDescription = "select recette.NOMRECETTE, NOMBOUTEILLE, INGREDIENTRECETTE.QTYSHOT,INGREDIENT.BOUTEILLEPRESENTE,INGREDIENT.QTYRESTANTE from INGREDIENT INNER JOIN INGREDIENTRECETTE ON INGREDIENT.CODEBOUTEILLE = INGREDIENTRECETTE.CODEBOUTEILLE INNER JOIN RECETTE ON INGREDIENTRECETTE.CODERECETTE = RECETTE.CODERECETTE WHERE RECETTE.CODERECETTE = " + i;

                stm1 = conn_.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE,ResultSet.CONCUR_READ_ONLY);

                resultSet = stm1.executeQuery(requeteDescription);
                String nom = null;
                String description = "";
                boolean drinkPossible = true;
                while(resultSet.next())
                {

                    nom = resultSet.getString(1);
                    description += resultSet.getString(3) +" oz " + resultSet.getString(2) +", ";
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
                hashMap.put("note", "0");
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
        SimpleAdapter simpleAdapter=new SimpleAdapter(this, arrayListDrink,R.layout.custom_list_drink,from,to);
        listDrinkLVIEW.setAdapter(simpleAdapter);//sets the adapter for listView
        final TextView triNoteBTN=findViewById(R.id.triNote_BTN);
        triNoteBTN.setText("A-B");
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

        int list = arrayListCart.size();
        for (int i = 0; i < list ; i++)
        {
            String  line = String.valueOf(arrayListCart.get(i));
            System.out.println(line);
        }
        DemanderNote();
        arrayListCart.clear();
        selectedCartPositions.clear();
        fillCartList();
    }

    void DemanderNote()
    {
        notesLYT.setVisibility(View.VISIBLE);
    }

    void AnnulerNote() {
        notesLYT.setVisibility(View.INVISIBLE);
        note=0;

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
    }

    void EnvoyerNote() {



        faireToast("Merci d'avoir noté: "+note);
        notesLYT.setVisibility(View.INVISIBLE);
        note=0;
    }

    void TrierEtoileHaut()
    {
        Collections.sort(arrayListDrink, new Comparator<HashMap<String,String>>()
        {
            public int compare(HashMap<String,String> o1,
                               HashMap<String,String> o2)
            {
                    float o1note = Float.valueOf(o1.get("note"));
                    float o2note = Float.valueOf(o2.get("note"));
                    if (o1note < o2note) {
                        return -1;
                    } else if (o1note > o2note) {
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
                    float o1note = Float.valueOf(o1.get("note"));
                    float o2note = Float.valueOf(o2.get("note"));
                    if (o1note < o2note) {
                        return 1;
                    } else if (o1note > o2note) {
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

    void refreshCartItemCount()
    {
        final TextView itemCountTXT=findViewById(R.id.cartItemsCount_TXT);
        itemCountTXT.setText(Integer.toString(arrayListCart.size()));
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
}