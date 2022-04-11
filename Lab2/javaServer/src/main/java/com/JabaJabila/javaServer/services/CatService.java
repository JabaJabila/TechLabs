package com.JabaJabila.javaServer.services;

import com.JabaJabila.javaServer.entities.Cat;
import com.JabaJabila.javaServer.repository.ICatRepository;
import com.JabaJabila.javaServer.repository.IOwnerRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Date;

@Service
public class CatService {
    @Autowired
    private ICatRepository catRepository;

    @Autowired
    private IOwnerRepository ownerRepository;

    public Cat createCat(String name, String breed, Date birthdate) {
        Cat cat = new Cat();
        if (name == null)
            throw new IllegalArgumentException("name can't be null");

        if (breed == null)
            throw new IllegalArgumentException("breed can't be null");

        if (birthdate == null)
            throw new IllegalArgumentException("birthdate can't be null");

        cat.setName(name);
        cat.setBreed(breed);
        cat.setBirthdate(birthdate);
        return catRepository.save(cat);
    }

    public Iterable<Cat> getAllCats() {
        return catRepository.findAll();
    }

    public Cat findCat(Long id) {
        return catRepository.findById(id).get();
    }

    public void deleteCat(Long id) {
        Cat cat = findCat(id);
        catRepository.delete(cat);
        ownerRepository.save(ownerRepository.findById(cat.getOwner().getOwnerId()).get());
    }
}