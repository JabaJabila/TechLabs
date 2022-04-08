package com.JabaJabila.javaServer.dto;

import com.JabaJabila.javaServer.entities.CatColor;

import java.util.Date;

public class CatCreationInfo {
    private Long catId;
    private String name;
    private CatColor color;
    private String breed;
    private Date birthdate;

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
}
