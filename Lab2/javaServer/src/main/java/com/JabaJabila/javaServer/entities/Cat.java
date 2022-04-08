package com.JabaJabila.javaServer.entities;

import javax.persistence.*;
import java.util.Date;

@Entity
public class Cat {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long catId;
    private String name;
    private CatColor color;
    private String breed;
    private Date birthdate;

    @ManyToOne
    private Owner owner;

    public Long getCatId() {
        return catId;
    }

    public void setCatId(Long catId) {
        this.catId = catId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public CatColor getColor() {
        return color;
    }

    public void setColor(CatColor color) {
        this.color = color;
    }

    public String getBreed() {
        return breed;
    }

    public void setBreed(String breed) {
        this.breed = breed;
    }

    public Date getBirthdate() {
        return birthdate;
    }

    public void setBirthdate(Date birthdate) {
        this.birthdate = birthdate;
    }

    public Owner getOwner() {
        return owner;
    }

    public void setOwner(Owner owner) {
        this.owner = owner;
    }
}

