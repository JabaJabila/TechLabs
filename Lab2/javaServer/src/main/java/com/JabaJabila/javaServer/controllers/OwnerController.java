package com.JabaJabila.javaServer.controllers;

import com.JabaJabila.javaServer.models.OwnerCreationInfo;
import com.JabaJabila.javaServer.models.OwnerViewModel;
import com.JabaJabila.javaServer.entities.Cat;
import com.JabaJabila.javaServer.entities.Owner;
import com.JabaJabila.javaServer.services.CatService;
import com.JabaJabila.javaServer.services.OwnerService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.*;

@RestController
@RequestMapping("owner")
public class OwnerController {

    @Autowired
    private OwnerService ownerService;

    @Autowired
    private CatService catService;

    @PostMapping(
            path = "create-owner",
            consumes = {MediaType.APPLICATION_XML_VALUE, MediaType.APPLICATION_JSON_VALUE},
            produces = {MediaType.APPLICATION_XML_VALUE, MediaType.APPLICATION_JSON_VALUE})
    public ResponseEntity<OwnerViewModel> CreateOwner(@RequestBody OwnerCreationInfo ownerInfo) {
        try {
            Owner owner = ownerService.createOwner(ownerInfo.getName(), ownerInfo.getBirthdate());
            return new ResponseEntity(OwnerViewModel.CreateFromEntity(owner), HttpStatus.OK);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(null);
        }
    }

    @GetMapping("/all")
    public ResponseEntity<List<OwnerViewModel>> getAll() {
        try {
            List<OwnerViewModel> ownerViews = new ArrayList<>();
            for (Owner owner: ownerService.getAllOwners()) {
                ownerViews.add(OwnerViewModel.CreateFromEntity(owner));
            }
            return new ResponseEntity(ownerViews, HttpStatus.OK);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(null);
        }
    }

    @GetMapping("/{id}")
    public ResponseEntity<OwnerViewModel> getOwner(@PathVariable Long id) {
        try {
            return new ResponseEntity(OwnerViewModel.CreateFromEntity(ownerService.findOwner(id)), HttpStatus.OK);
        } catch (RuntimeException e) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(null);
        }
    }

    @DeleteMapping("delete")
    public HttpStatus deleteOwner(@RequestParam Long id) {
        try {
            ownerService.deleteOwner(id);
            return HttpStatus.OK;
        } catch (RuntimeException e) {
            return HttpStatus.NOT_FOUND;
        }
    }

    @PostMapping("add/{ownerId}")
    public HttpStatus addCat(@PathVariable Long ownerId, @RequestParam Long catId) {
        try {
            ownerService.addCat(ownerService.findOwner(ownerId), catService.findCat(catId));
            return HttpStatus.OK;
        } catch (RuntimeException e) {
            return HttpStatus.NOT_FOUND;
        }
    }

    @PostMapping("add-many/{ownerId}")
    public HttpStatus addCats(@PathVariable Long ownerId, @RequestParam List<Long> catIds) {
        try {
            List<Cat> cats = new ArrayList<Cat>();
            for (Long id: catIds) {
                cats.add(catService.findCat(id));
            }

            ownerService.addCats(ownerService.findOwner(ownerId), cats);
            return HttpStatus.OK;

        } catch (RuntimeException e) {
            return HttpStatus.NOT_FOUND;
        }
    }
}
