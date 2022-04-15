package com.JabaJabila.javaServer.repository;

import com.JabaJabila.javaServer.entities.Owner;
import org.springframework.data.repository.CrudRepository;

public interface IOwnerRepository extends CrudRepository<Owner, Long> {
}