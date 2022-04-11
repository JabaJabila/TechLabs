package com.JabaJabila.javaServer.dto;
import com.JabaJabila.javaServer.entities.Cat;

import java.util.Date;

public class CatViewModel {
    private Long catId;
    private String name;
    private String breed;
    private Date birthdate;
    private Long ownerId;

    public Long getCatId() {
        return catId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
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

    public Long getOwnerId() {
        return ownerId;
    }

    public void setOwnerId(Long ownerId) {
        this.ownerId = ownerId;
    }

    public static CatViewModel CreateFromEntity(Cat cat) {
        CatViewModel result = new CatViewModel();
        result.catId = cat.getCatId();
        result.name = cat.getName();
        result.breed = cat.getBreed();
        result.birthdate = cat.getBirthdate();

        if (cat.getOwner() != null)
            result.ownerId = cat.getOwner().getOwnerId();
        else
            result.ownerId = null;

        return result;
    }
}
