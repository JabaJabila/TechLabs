package com.JabaJabila.javaServer.dto;

import com.JabaJabila.javaServer.entities.Cat;
import com.JabaJabila.javaServer.entities.Owner;

import java.util.ArrayList;
import java.util.Date;

public class OwnerViewModel {
    private Long ownerId;
    private String name;
    private Date birthdate;
    private ArrayList<CatViewModel> cats;

    public Long getOwnerId() {
        return ownerId;
    }

    public void setOwnerId(Long ownerId) {
        this.ownerId = ownerId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Date getBirthdate() {
        return birthdate;
    }

    public void setBirthdate(Date birthdate) {
        this.birthdate = birthdate;
    }

    public ArrayList<CatViewModel> getCats() { return cats; }

    public void setCats(ArrayList<CatViewModel> cats) { this.cats = cats; }

    public static OwnerViewModel CreateFromEntity(Owner owner) {
        OwnerViewModel result = new OwnerViewModel();
        result.ownerId = owner.getOwnerId();
        result.name = owner.getName();
        result.birthdate = owner.getBirthdate();

        ArrayList<CatViewModel> catViews = new ArrayList<>();

        if (owner.getCats() == null) return result;
        for (Cat cat: owner.getCats()) {
            catViews.add(CatViewModel.CreateFromEntity(cat));
        }

        result.cats = catViews;
        return result;
    }
}
