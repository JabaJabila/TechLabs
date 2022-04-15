package com.JabaJabila.javaServer.services;

import com.JabaJabila.javaServer.entities.Cat;
import com.JabaJabila.javaServer.entities.Owner;
import com.JabaJabila.javaServer.repository.ICatRepository;
import com.JabaJabila.javaServer.repository.IOwnerRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Date;

@Service
public class OwnerService {
    @Autowired
    private IOwnerRepository ownerRepository;

    @Autowired
    private ICatRepository catRepository;

    public Owner createOwner(String name, Date birthdate) {
        Owner owner = new Owner();
        if (name == null)
            throw new IllegalArgumentException("name can't be null");

        if (birthdate == null)
            throw new IllegalArgumentException("birthdate can't be null");

        owner.setName(name);
        owner.setBirthdate(birthdate);
        return ownerRepository.save(owner);
    }

    public Iterable<Owner> getAllOwners() {
        return ownerRepository.findAll();
    }

    public Owner findOwner(Long id) {
        return ownerRepository.findById(id).get();
    }

    public void deleteOwner(Long id) {
        ownerRepository.delete(findOwner(id));
    }

    public Owner addCat(Owner owner, Cat cat) {
        if (owner == null)
            throw new IllegalArgumentException("owner can't be null");

        if (cat == null)
            throw new IllegalArgumentException("cat can't be null");

        cat.setOwner(owner);
        catRepository.save(cat);
        ownerRepository.save(owner);

        return owner;
    }

    public Owner addCats(Owner owner, Iterable<Cat> cats) {
        if (owner == null)
            throw new IllegalArgumentException("owner can't be null");

        if (cats == null)
            throw new IllegalArgumentException("cats collection can't be null");

        for (Cat cat : cats) {
            if (cat == null)
                throw new IllegalArgumentException("cat can't be null");

            cat.setOwner(owner);
            catRepository.save(cat);
            owner = ownerRepository.findById(owner.getOwnerId()).get();
            ownerRepository.save(owner);
        }

        ownerRepository.save(owner);
        return owner;
    }
}
